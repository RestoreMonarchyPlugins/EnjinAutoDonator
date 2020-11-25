using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WebDonationsToEnjinAutoDonator
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class EnjinAutoDonatorConfiguration
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
        public bool PayUconomyMoneyOnce { get; set; }

        public string DatabaseAddress { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseName { get; set; }
        public int DatabasePort { get; set; }

        public string PurchasesTableName { get; set; }

        public string SteamIDIdentifier { get; set; }
        public Package[] Packages { get; set; }

        public class Package
        {
            [XmlAttribute]
            public int EnjinItemId { get; set; }
            [XmlAttribute]
            public bool DisableBroadcast { get; set; }

            [XmlArrayItem("GroupID")]
            public string[] AddGroups { get; set; }
            [XmlArrayItem("GroupID")]
            public string[] RemoveGroups { get; set; }
            [XmlArrayItem("Command")]
            public string[] Commands { get; set; }
            public decimal UconomyMoney { get; set; }
        }
    }
}
