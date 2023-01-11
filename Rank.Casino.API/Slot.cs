using Rank.Casino.Data;
using Rank.Casino.Data.Types;

namespace Rank.Casino.API
{
    namespace Rank.Casino.API
    {
        public class Slot
        {
            private SQL _sqlDL;

            public Slot(string sqlConnString)
            {
                _sqlDL = new SQL(sqlConnString);
            }

            public Player GetPlayer(int playerId)
            {
                return _sqlDL.GetPlayerById(playerId);
            }

            public Player GetPlayer(string username)
            {
                return _sqlDL.GetPlayerByUsername(username);
            }

            public Player GetPlayerBalance(int playerId)
            {
                return _sqlDL.GetPlayerById(playerId);
            }

            public List<Transaction> GetPlayerTransactions(string username, int count)
            {
                return _sqlDL.GetPlayerTransctionsById(username, count);
            }

            public Balance UpdatePlayerBalance(int playerId, float amount, string tranactionType)
            {
                if (tranactionType == "WAGER")
                    amount = amount * -1;

                var transId = _sqlDL.UpdateBalance(playerId, amount, tranactionType);

                return new Balance
                {
                    //Get updated Balance
                    Amount = GetPlayer(playerId).Balance,
                    TransactionId = transId
                };
            }

            public float RefreshPlayerBalance(int playerId)
            {
               return _sqlDL.RefreshBalances(playerId);
            }
        }
    }
}