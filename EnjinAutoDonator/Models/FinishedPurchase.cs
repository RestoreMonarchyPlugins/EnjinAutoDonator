using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Models
{
    public class FinishedPurchase
    {
        public int Id { get; set; }
        public int PurchaseDate { get; set; }

        public string SteamId { get; set; }
        public string SteamName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ServerId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
