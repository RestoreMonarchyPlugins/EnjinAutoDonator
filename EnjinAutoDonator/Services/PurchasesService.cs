using Newtonsoft.Json.Linq;
using Rocket.API;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Core.Steam;
using Rocket.Core.Utils;
using Rocket.Unturned.Chat;
using EnjinAutoDonator.Helpers;
using EnjinAutoDonator.Models;
using EnjinAutoDonator.Providers;
using EnjinAutoDonator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace EnjinAutoDonator.Services
{
    public class PurchasesService : MonoBehaviour
    {
        private EnjinAutoDonatorPlugin pluginInstance => EnjinAutoDonatorPlugin.Instance;

        public IPurchasesDatabaseProvider Database { get; set; }
        private EnjinHttpClient HttpClient { get; set; }

        public Timer RefreshTimer { get; set; }
        private DateTime SinceDate => DateTime.UtcNow.AddDays(-pluginInstance.Configuration.Instance.LookDaysBack);

        void Awake()
        {
            Database = new MySQLPurchasesDatabaseProvider();
            HttpClient = new EnjinHttpClient();

            RefreshTimer = new Timer(pluginInstance.Configuration.Instance.RefreshTimeMiliseconds);
            RefreshTimer.Elapsed += CheckForUpdate;
        }

        void Start()
        {
            Database.Initialize();
            RefreshTimer.Start();
            Logger.Log("Purchases service started", ConsoleColor.Yellow);
        }

        void OnDestroy()
        {            
            RefreshTimer.Elapsed -= CheckForUpdate;
            RefreshTimer.Dispose();
        }

        public void CheckForUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                int epoch = (int)(SinceDate - new DateTime(1970, 1, 1)).TotalSeconds;
                
                var json = EnjinRequestsHelper.GetPurchasesResult(HttpClient, epoch);
                if (json == null)
                    return;

                var finishedPurchases = Database.GetPurchases(epoch, pluginInstance.Configuration.Instance.ServerIdentifier);

                List<Tuple<JToken, JToken>> awaitingItems = new List<Tuple<JToken, JToken>>();
                foreach (var result in json["result"].AsJEnumerable())
                {
                    foreach (var item in result["items"].AsJEnumerable())
                    {
                        // skip if item is not operated on this server
                        if (!pluginInstance.Configuration.Instance.Features.Any(x => x.EnjinItemId == item["item_id"].ToObject<int>()))
                            continue;

                        if (!finishedPurchases.Any(x => x.PurchaseDate == result["purchase_date"].ToObject<int>() && x.ItemId == item["item_id"].ToObject<int>()))
                        {
                            awaitingItems.Add(new Tuple<JToken, JToken>(item, result));
                        }
                    }                    
                }

                if (awaitingItems.Count > 0)
                {
                    ProcessPurchases(awaitingItems);
                }
            } catch (Exception ex)
            {
                Logger.LogException(ex);
            }            
        }

        private void ProcessPurchases(List<Tuple<JToken, JToken>> purchases)
        {
            foreach (var purchase in purchases)
            {
                ProcessPurchase(purchase.Item1, purchase.Item2);
            }
        }

        private void ProcessPurchase(JToken item, JToken result)
        {
            var features = pluginInstance.Configuration.Instance.Features.FirstOrDefault(x => x.EnjinItemId == item["item_id"].ToObject<int>());
            if (features == null)
                return;

            string steamIdStr = item["variables_names"][pluginInstance.Configuration.Instance.SteamIDIdentifier].ToString();

            string steamName = null;
            if (ulong.TryParse(steamIdStr, out ulong steamId))
            {
                try
                {
                    Profile profile = new Profile(steamId);
                    steamName = profile.SteamID;
                }
                catch (Exception e)
                {
                    Logger.LogException(e, $"An exception occurated while downloading {steamId} name from Steam");
                }

                TaskDispatcher.QueueOnMainThread(() => ExecuteFeatures(new RocketPlayer(steamId.ToString(), steamName), features, item["item_name"].ToString()));
            }

            var purchase = new FinishedPurchase()
            {
                ItemId = item["item_id"].ToObject<int>(),
                ItemName = item["item_name"].ToString(),
                SteamId = steamIdStr,
                SteamName = steamName,
                PurchaseDate = result["purchase_date"].ToObject<int>(),
                ServerId = pluginInstance.Configuration.Instance.ServerIdentifier,
                CreateDate = DateTime.UtcNow
            };

            bool flag = Database.ContainsPurchase(purchase.PurchaseDate, purchase.ItemId);
            Database.FinishPurchase(purchase);

            if (!flag && !string.IsNullOrEmpty(pluginInstance.Configuration.Instance.DiscordWebhookUrl))
                DiscordHelper.SendNotification(purchase);
        }

        private void ExecuteFeatures(RocketPlayer player, ShopItemFeatures features, string itemName)
        {
            if (features.Commands != null && features.Commands.Length > 0)
            {
                foreach (var command in features.Commands)
                {
                    R.Commands.Execute(new ConsolePlayer(), command.Replace("{steamid}", player.Id).Replace("{steamname}", player.DisplayName));
                }
            }

            if (features.AddGroups != null && features.AddGroups.Length > 0)
            {
                foreach (var group in features.AddGroups)
                {
                    R.Permissions.AddPlayerToGroup(group, player);
                }
            }

            if (features.RemoveGroups != null && features.RemoveGroups.Length > 0)
            {
                foreach (var group in features.RemoveGroups)
                {
                    R.Permissions.RemovePlayerFromGroup(group, player);
                }
            }

            if (features.UconomyMoney > 0)
            {
                if (RocketPlugin.IsDependencyLoaded("Uconomy"))
                {
                    // using helper to avoid depdency error message in case uconomy not installed
                    UconomyHelper.PayMoney(player.Id, features.UconomyMoney);
                } else
                {
                    Logger.LogWarning($"Failed to pay {features.UconomyMoney} money to {player.DisplayName} {player.Id}, because Uconomy is not installed");
                }
            }

            if (!features.DisableBroadcast && player.DisplayName != null)
            {
                UnturnedChat.Say(pluginInstance.Translate("PurchaseBroadcast", player.DisplayName, itemName), pluginInstance.MessageColor);
            }
        }
    }
}
