﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using static WebDonationsToEnjinAutoDonator.EnjinAutoDonatorConfiguration;

namespace WebDonationsToEnjinAutoDonator
{
    class Program
    {
        public static Program Instance { get; private set; }
        public const string Version = "v1.0.1";

        static void Main(string[] args)
        {
            Instance = new Program();
            Instance.Run();
        }

        private string directory => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        private void Run()
        {
            Console.WriteLine($"WebDonationsToEnjinAutoDonator {Version}");
            Console.WriteLine("Write WebDonations configuration file name");
            string fileName = Console.ReadLine();
            string webDonationsPath = Path.Combine(directory, fileName);
            string enjinAutoDonatorPath = Path.Combine(directory, "EnjinAutoDonator.configuration.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(WebDonationsConfiguration));
            XmlSerializer serializer2 = new XmlSerializer(typeof(EnjinAutoDonatorConfiguration));

            WebDonationsConfiguration webDonations;
            using (var reader = XmlReader.Create(webDonationsPath))
            {
                webDonations = serializer.Deserialize(reader) as WebDonationsConfiguration;
            }

            var config = new EnjinAutoDonatorConfiguration
            {
                WebsiteUrl = webDonations.Enjin.websiteUrl,
                APIKey = webDonations.Enjin.apiKey,
                PresetId = webDonations.Enjin.presetId,
                DiscordWebhookUrl = webDonations.discordWebhookURL,
                ServerIdentifier = "PVP1",
                MessageColor = "magenta",
                LookDaysBack = 7,
                SteamIDIdentifier = "steamID",
                DiscordWebhookColor = "#843da4",
                RefreshTimeMiliseconds = 30000,
                RequestTimeoutMiliseconds = 10000,
                PayUconomyMoneyOnce = true,
                DatabaseAddress = "127.0.0.1",
                DatabaseName = "unturned",
                DatabasePort = 3306,
                DatabaseUsername = "root",
                DatabasePassword = "password123",
                PurchasesTableName = "FinishedPurchases"
            };

            var packages = new List<Package>();
            foreach (var webDonationsPackage in webDonations.StorePackages)
            {
                var package = new Package
                {
                    EnjinItemId = webDonationsPackage.packageStoreID
                };

                var commands = new List<string>();
                var addGroups = new List<string>();
                var removeGroups = new List<string>();

                foreach (var command in webDonationsPackage.commands)
                {
                    if (command.StartsWith("p add", StringComparison.OrdinalIgnoreCase))
                    {
                        addGroups.Add(command.Split(' ').LastOrDefault());
                    } else if (command.StartsWith("p remove", StringComparison.OrdinalIgnoreCase))
                    {
                        removeGroups.Add(command.Split(' ').LastOrDefault());
                    } else if (command.StartsWith("pay", StringComparison.OrdinalIgnoreCase) || command.StartsWith("apay", StringComparison.OrdinalIgnoreCase))
                    {
                        if (decimal.TryParse(command.Split(' ').LastOrDefault(), out decimal result))
                        {
                            package.UconomyMoney = result;
                        }
                    } else
                    {
                        commands.Add(command.Replace("{steamid}", "{steamid}", StringComparison.OrdinalIgnoreCase));
                    }
                }

                package.Commands = commands.ToArray();
                package.AddGroups = addGroups.ToArray();
                package.RemoveGroups = removeGroups.ToArray();
                packages.Add(package);
            }

            config.Packages = packages.ToArray();

            using (var streamWriter = new StreamWriter(enjinAutoDonatorPath))
            {
                serializer2.Serialize(streamWriter, config);
            }
            Console.WriteLine($"Successfully migrated WebDonations configuration to {enjinAutoDonatorPath}");
            Console.ReadKey();
        }
    }
}
