using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnjinAutoDonator.Models
{
    public class ShopItemFeatures
    {
        [XmlAttribute]
        public int EnjinItemId { get; set; }
        [XmlAttribute]
        public bool DisableBroadcast { get; set; }

        [XmlArrayItem("GroupID")]
        public string[] AddGroups { get; set; }
        [XmlArrayItem("GroupID")]
        public string[] RemoveGroups { get; set; }
        [XmlElement("Command")]
        public string[] Commands { get; set; }
        public decimal UconomyMoney { get; set; }        
    }
}
