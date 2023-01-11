namespace Rank.Casino.Data.Types
{
    public static class Constants
    {
        public const string ColumnBalance = "Balance";
        public const string ColumnName = "Name";
        public const string ColumnPlayerId = "PlayerId";
        public const string ColumnUsername = "Username";

        public const string ColumnId = "Id";
        public const string ColumnTransactionType = "TransactionType";
        public const string ColumnCreatedDate = "CreatedDate";
        public const string ColumnAmount = "Amount";
        public const string ColumnState = "State";
    }

    public class Transaction
    {
        public DateTime CreateDate { get; set; }
        public int TransactionId { get; set; }  
        public int PlayerId { get; set; }

        public string TransactionType { get; set; }

        public float Amount { get; set; }
        public string State { get; set; }

        public enum States
        {         
            Initiated,
            InProgress,
            Completed,
            Error
        }
    }

    public class Balance
    {
        public float Amount { get; set; } 
        public int TransactionId { get; set; }
    }

    public class Player
    {
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public float Balance { get; set; }
    }   
}