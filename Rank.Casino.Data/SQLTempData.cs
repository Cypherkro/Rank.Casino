using Rank.Casino.Data.Types;
using System.Data;
using static Rank.Casino.Data.Types.Transaction;

namespace Rank.Casino.Data
{   
    public static class TempDataStoredProc
    {
        private static DataTable _players = new DataTable("Players");
        private static DataTable _transactions = new DataTable("Transactions");

        public static void CreateTempData()
        {
            _players.Columns.Add(Constants.ColumnPlayerId);
            _players.Columns.Add(Constants.ColumnName);
            _players.Columns.Add(Constants.ColumnBalance);
            _players.Columns.Add(Constants.ColumnUsername);

            var playersRow = _players.Rows.Add("");
            playersRow[Constants.ColumnPlayerId] = "1";
            playersRow[Constants.ColumnUsername] = "JohnD";
            playersRow[Constants.ColumnName] = "John Doe";
            playersRow[Constants.ColumnBalance] = "100";

            var playersRowJane = _players.Rows.Add("");
            playersRowJane[Constants.ColumnPlayerId] = "2";
            playersRowJane[Constants.ColumnUsername] = "JaneD";
            playersRowJane[Constants.ColumnName] = "Jane Doe";
            playersRowJane[Constants.ColumnBalance] = "120";


            _transactions.Columns.Add(Constants.ColumnId);
            _transactions.Columns.Add(Constants.ColumnPlayerId);
            _transactions.Columns.Add(Constants.ColumnAmount);
            _transactions.Columns.Add(Constants.ColumnTransactionType);
            _transactions.Columns.Add(Constants.ColumnCreatedDate);
            _transactions.Columns.Add(Constants.ColumnState);

            var transactionRow = _transactions.Rows.Add("");
            transactionRow[Constants.ColumnPlayerId] = 1;
            transactionRow[Constants.ColumnId] = 1;
            transactionRow[Constants.ColumnAmount] = "100";
            transactionRow[Constants.ColumnTransactionType] = "WIN";
            transactionRow[Constants.ColumnState] = States.Completed;
            transactionRow[Constants.ColumnCreatedDate] = DateTime.UtcNow;
        }

        #region Functions representing SQL Calls/StoredProcs to return DataSets

        public static DataTable GetPlayers()
        {
            return _players;
        }

        public static DataRow GetPlayerByUsername(string username)
        {
            //SQL call to Retrieve a player by ID, should be a SQL call, but will pull from memory for POC
            foreach (DataRow row in _players.Rows)
            {
                if (row[Constants.ColumnUsername].ToString() == username)
                {
                    return row;
                }
            }

            throw new InvalidDataException("Player not found for User: " + username);
        }

        public static DataRow GetPlayerById(int playerId)
        {
            //SQL call to Retrieve a player by ID, should be a SQL call, but will pull from memory for POC
            foreach (DataRow row in _players.Rows)
            {
                if (int.Parse(row[Constants.ColumnPlayerId].ToString()) == playerId)
                {
                    return row;
                }
            }

            throw new Exception("Player not found for Id: " + playerId);
        }

        public static int CreateTransaction(int playerId, float amount, string transactionType)
        {
            var transacionId = _transactions.Rows.Count + 1;//simulated SQL Column Identity
            var transactionRow = _transactions.Rows.Add("");
            transactionRow[Constants.ColumnPlayerId] = playerId;
            transactionRow[Constants.ColumnState] = States.Initiated;
            //todo save current state to DB to created initial Transaction and set State to Initiated.
            //Audit and tracking for multiple Transactons and system failure. Recovery and Continue as additional calls will be made 
            //and maintaining a State engine per transaction allows for this. Next State should be InProgress if calls are post 1 second

            try
            {
                transactionRow[Constants.ColumnId] = transacionId;
                transactionRow[Constants.ColumnAmount] = amount;
                transactionRow[Constants.ColumnTransactionType] = transactionType;
                transactionRow[Constants.ColumnCreatedDate] = DateTime.UtcNow;
                transactionRow[Constants.ColumnState] = States.Completed;
                return transacionId;
            }
            catch(Exception ex)
            {
                transactionRow[Constants.ColumnState] = States.Error;//todo
                throw ex;
            }
        }

        public static List<DataRow> GetPlayerLastxTransactions(string username, int count)
        {
            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow trans in _transactions.Rows)
            {
                var player = GetPlayerByUsername(username);

                if (trans[Constants.ColumnPlayerId].ToString() == player[Constants.ColumnPlayerId])
                {
                    rows.Add(trans);
                    count--;
                    if (count == 0)
                        break;
                }
            }

            return rows;
        }

        public static List<DataRow> GetPlayerTransactions(int playerId)
        {
            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow trans in _transactions.Rows)
            {
                if (trans[Constants.ColumnId].ToString() == playerId.ToString())
                {
                    rows.Add(trans);
                }
            }

            return rows;
        }

        #endregion
    }
}