using System;
using System.Collections.Generic;
using System.Text;

namespace WebDonationsToEnjinAutoDonator
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class WebDonationsConfiguration
    {

        private WebDonationsConfigurationWooCommerce wooCommerceField;

        private WebDonationsConfigurationEnjin enjinField;

        private WebDonationsConfigurationTebex tebexField;

        private string discordWebhookURLField;

        private WebDonationsConfigurationPackage[] storePackagesField;

        private bool checkTopDonatorPreviousMonthsField;

        private string licenseKeyField;

        /// <remarks/>
        public WebDonationsConfigurationWooCommerce WooCommerce
        {
            get
            {
                return this.wooCommerceField;
            }
            set
            {
                this.wooCommerceField = value;
            }
        }

        /// <remarks/>
        public WebDonationsConfigurationEnjin Enjin
        {
            get
            {
                return this.enjinField;
            }
            set
            {
                this.enjinField = value;
            }
        }

        /// <remarks/>
        public WebDonationsConfigurationTebex Tebex
        {
            get
            {
                return this.tebexField;
            }
            set
            {
                this.tebexField = value;
            }
        }

        /// <remarks/>
        public string discordWebhookURL
        {
            get
            {
                return this.discordWebhookURLField;
            }
            set
            {
                this.discordWebhookURLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Package", IsNullable = false)]
        public WebDonationsConfigurationPackage[] StorePackages
        {
            get
            {
                return this.storePackagesField;
            }
            set
            {
                this.storePackagesField = value;
            }
        }

        /// <remarks/>
        public bool checkTopDonatorPreviousMonths
        {
            get
            {
                return this.checkTopDonatorPreviousMonthsField;
            }
            set
            {
                this.checkTopDonatorPreviousMonthsField = value;
            }
        }

        /// <remarks/>
        public string LicenseKey
        {
            get
            {
                return this.licenseKeyField;
            }
            set
            {
                this.licenseKeyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WebDonationsConfigurationWooCommerce
    {

        private bool enabledField;

        private string websiteUrlField;

        private string consumerKeyField;

        private string consumerSecretField;

        private byte topDonatorPackageIdField;

        /// <remarks/>
        public bool enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        /// <remarks/>
        public string websiteUrl
        {
            get
            {
                return this.websiteUrlField;
            }
            set
            {
                this.websiteUrlField = value;
            }
        }

        /// <remarks/>
        public string consumerKey
        {
            get
            {
                return this.consumerKeyField;
            }
            set
            {
                this.consumerKeyField = value;
            }
        }

        /// <remarks/>
        public string consumerSecret
        {
            get
            {
                return this.consumerSecretField;
            }
            set
            {
                this.consumerSecretField = value;
            }
        }

        /// <remarks/>
        public byte topDonatorPackageId
        {
            get
            {
                return this.topDonatorPackageIdField;
            }
            set
            {
                this.topDonatorPackageIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WebDonationsConfigurationEnjin
    {

        private bool enabledField;

        private string websiteUrlField;

        private string apiKeyField;

        private int presetIdField;

        private byte topDonatorPackageIdField;

        /// <remarks/>
        public bool enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        /// <remarks/>
        public string websiteUrl
        {
            get
            {
                return this.websiteUrlField;
            }
            set
            {
                this.websiteUrlField = value;
            }
        }

        /// <remarks/>
        public string apiKey
        {
            get
            {
                return this.apiKeyField;
            }
            set
            {
                this.apiKeyField = value;
            }
        }

        /// <remarks/>
        public int presetId
        {
            get
            {
                return this.presetIdField;
            }
            set
            {
                this.presetIdField = value;
            }
        }

        /// <remarks/>
        public byte topDonatorPackageId
        {
            get
            {
                return this.topDonatorPackageIdField;
            }
            set
            {
                this.topDonatorPackageIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WebDonationsConfigurationTebex
    {

        private bool enabledField;

        private string shopUrlField;

        private string secretKeyField;

        private byte topDonatorPackageIdField;

        /// <remarks/>
        public bool enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        /// <remarks/>
        public string shopUrl
        {
            get
            {
                return this.shopUrlField;
            }
            set
            {
                this.shopUrlField = value;
            }
        }

        /// <remarks/>
        public string secretKey
        {
            get
            {
                return this.secretKeyField;
            }
            set
            {
                this.secretKeyField = value;
            }
        }

        /// <remarks/>
        public byte topDonatorPackageId
        {
            get
            {
                return this.topDonatorPackageIdField;
            }
            set
            {
                this.topDonatorPackageIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WebDonationsConfigurationPackage
    {

        private int packageStoreIDField;

        private string[] commandsField;

        private byte subscriptionLengthField;

        private string[] removeCommandsField;

        /// <remarks/>
        public int packageStoreID
        {
            get
            {
                return this.packageStoreIDField;
            }
            set
            {
                this.packageStoreIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("command", IsNullable = false)]
        public string[] commands
        {
            get
            {
                return this.commandsField;
            }
            set
            {
                this.commandsField = value;
            }
        }

        /// <remarks/>
        public byte subscriptionLength
        {
            get
            {
                return this.subscriptionLengthField;
            }
            set
            {
                this.subscriptionLengthField = value;
            }
        }

        /// <remarks/>
        public string[] removeCommands
        {
            get
            {
                return this.removeCommandsField;
            }
            set
            {
                this.removeCommandsField = value;
            }
        }
    }


}
