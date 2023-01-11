using Newtonsoft.Json;

namespace Rank.Casino.TestHarness
{
    public static class TestCases
    {
        public static object GetPlayerBalance(int playerId)
        {
            var controller = new Rank.Casino.Web.Controllers.CasinoController(null);
            return JsonConvert.SerializeObject(controller.GetPlayerBalance(playerId));
        }

        public static object GetPlayerTransactions(string username)
        {
            var controller = new Rank.Casino.Web.Controllers.CasinoController(null);
            return JsonConvert.SerializeObject(controller.PostLast10Transactions(username));
        }

        public static object UpdatePlayerBalance(int playerId, float amount, string transactionType)
        {
            var controller = new Rank.Casino.Web.Controllers.CasinoController(null);
            return JsonConvert.SerializeObject(controller.PostUpdateBalance(playerId, amount, transactionType));
        }
    }
}
