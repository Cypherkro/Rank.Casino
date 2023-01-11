using Rank.Casino.Data.Types;
using System.Data;

namespace Rank.Casino.Data
{
    //todo Integrate to MSSQLDB:  currently running with test data in Memory as per POC
    public class SQL
    {
        public SQL(string connectionString)
        {
            //todo instantiate SQL Cnnection
        }

        /// <summary>
        /// Get the Player information
        /// </summary>
        /// <param name="playerId">The player id to search for</param>
        /// <returns>The Player Object</returns>
        /// <exception cref="Exception"></exception>
        public Player GetPlayerById(int playerId)
        {
            var player = TempDataStoredProc.GetPlayerById(playerId);

            try
            {
                return new Player()
                {
                    PlayerId = int.Parse(player[Constants.ColumnPlayerId].ToString()),
                    Balance = float.Parse(player[Constants.ColumnBalance].ToString()),
                    PlayerName = player[Constants.ColumnName].ToString()
                };
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid data for Player: " + ex.Message);
            }
        }

        public Player GetPlayerByUsername(string userName)
        {
            var player = TempDataStoredProc.GetPlayerByUsername(userName);

            try
            {
                return new Player()
                {
                    PlayerId = int.Parse(player[Constants.ColumnPlayerId].ToString()),
                    Balance = float.Parse(player[Constants.ColumnBalance].ToString()),
                    PlayerName = player[Constants.ColumnName].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid data for Player: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the Player's last Transactions
        /// </summary>
        /// <param name="playerId">The PlayerID</param>
        /// <param name="count">The amount of transactions to return</param>
        /// <returns>The list of transctions based on the parameters</returns>
        public List<Transaction> GetPlayerTransctionsById(string username, int count)
        {
            var transactions = new List<Transaction>();

            foreach (DataRow row in TempDataStoredProc.GetPlayerLastxTransactions(username, count))
            {
                var transaction = new Transaction()
                {
                    TransactionId = int.Parse(row[Constants.ColumnId].ToString()),
                    CreateDate = DateTime.Parse(row[Constants.ColumnCreatedDate].ToString()),
                    PlayerId = int.Parse(row[Constants.ColumnPlayerId].ToString()),
                    TransactionType = row[Constants.ColumnTransactionType].ToString(),
                    State = row[Constants.ColumnState].ToString(),
                    Amount = float.Parse(row[Constants.ColumnAmount].ToString())
                };

                //todo complete
                transactions.Add(transaction);
            }

            return transactions;
        }

        /// <summary>
        /// Update Players Balance based on the last Transaction
        /// </summary>
        /// <param name="playerId">The Player ID</param>
        /// <param name="amount">The Amount to update the Balance with</param>
        public int UpdateBalance(int playerId, float amount, string transactionType)
        {
            var transId = TempDataStoredProc.CreateTransaction(playerId, amount, transactionType);
            var player = TempDataStoredProc.GetPlayerById(playerId);
            if (player[Constants.ColumnBalance] == null)
                player[Constants.ColumnBalance] = 0;

            player[Constants.ColumnBalance] = float.Parse(player[Constants.ColumnBalance].ToString()) + amount;
            return transId;
        }

        /// <summary>
        /// Recalculates Players Balance based on all Transactions
        /// </summary>
        /// <param name="playerId"The Player Id></param>
        /// <returns>Returns the updated Player Balance</returns>
        public float RefreshBalances(int playerId)
        {
            var player = TempDataStoredProc.GetPlayerById(playerId);
            var transacions = TempDataStoredProc.GetPlayerTransactions(playerId);
            var balance = 0f;

            foreach (var row in transacions)
            {
                try
                {
                    balance += float.Parse(player[Constants.ColumnAmount].ToString());
                }
                catch (Exception ex)
                {
                    //todo mark as "REVIEW"
                }
            }

            return balance;
        }
    }
}
