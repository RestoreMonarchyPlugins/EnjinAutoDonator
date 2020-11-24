using EnjinAutoDonator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Providers
{
    public interface IPurchasesDatabaseProvider
    {
        void Initialize();
        IEnumerable<FinishedPurchase> GetPurchases(int sinceEpoch, string serverId);
        void FinishPurchase(FinishedPurchase purchase);
        bool ContainsPurchase(int epoch, int itemId);
    }
}
