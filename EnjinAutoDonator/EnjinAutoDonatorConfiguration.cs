using Rocket.API;
using EnjinAutoDonator.Models;

namespace EnjinAutoDonator
{
    public class EnjinAutoDonatorConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }

        public string ServerIdentifier { get; set; }
        public string WebsiteUrl { get; set; }
        public string APIKey { get; set; }
        public int PresetId { get; set; }

        public string DiscordWebhookUrl { get; set; }
        public string DiscordWebhookColor { get; set; }

        public int LookDaysBack { get; set; }
        public double RefreshTimeMiliseconds { get; set; }
        public int RequestTimeoutMiliseconds { get; set; }

        public string DatabaseAddress { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseName { get; set; }
        public int DatabasePort { get; set; }

        public string PurchasesTableName { get; set; }

        public string SteamIDIdentifier { get; set; }
        public ShopItemFeatures[] Features { get; set; }


        public void LoadDefaults()
        {            
            MessageColor = "magenta";
            ServerIdentifier = "PVP1";
            WebsiteUrl = "http://www.yourwebsite.com/api/v1/api.php";
            APIKey = "APIKEY";
            PresetId = 248723;

            DiscordWebhookUrl = "";
            DiscordWebhookColor = "";

            LookDaysBack = 7;
            RefreshTimeMiliseconds = 30000;
            RequestTimeoutMiliseconds = 10000;

            DatabaseAddress = "127.0.0.1";
            DatabaseUsername = "root";
            DatabasePassword = "password123";
            DatabaseName = "unturned";
            DatabasePort = 3306;

            PurchasesTableName = "FinishedPurchases";

            SteamIDIdentifier = "steamID";
            Features = new ShopItemFeatures[]
            {
                new ShopItemFeatures()
                {
                    EnjinItemId = 123,
                    AddGroups = new string[]
                    {
                        "vip"
                    },
                    Commands = new string[]
                    {
                        "/airdrop"
                    },
                    UconomyMoney = 300,
                    DisableBroadcast = false
                }
            };

        }
    }
}