using fr34kyn01535.Uconomy;

namespace EnjinAutoDonator.Helpers
{
    public class UconomyHelper
    {
        public static void PayMoney(string steamId, decimal amount)
        {
            Uconomy.Instance.Database.IncreaseBalance(steamId, amount);
        }
    }
}
