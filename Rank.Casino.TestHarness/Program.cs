using Rank.Casino.TestHarness;

Rank.Casino.Data.TempDataStoredProc.CreateTempData();

while (true)
{
    try
    {
        Console.WriteLine("Select Function");
        Console.WriteLine("1 Get Player Balance\n2 Get Player Last 10 Transaction \n3 Update Balance");
        var response = Console.ReadLine();
        switch (response)
        {
            case "1":
                Console.WriteLine("Enter PlayerId Default is 1");
                var playerId = int.Parse(Console.ReadLine());
                Console.WriteLine(TestCases.GetPlayerBalance(playerId));
                break;

            case "2":
                Console.WriteLine("Enter Username Default is JD");
                var username = Console.ReadLine();
                Console.WriteLine(TestCases.GetPlayerTransactions(username));
                break;

            case "3":
                Console.WriteLine("Enter PlayerId Default is 1");
                var playerIdUpdate = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Amount");
                var amount = float.Parse(Console.ReadLine());

                Console.WriteLine("Enter Transaction Type WIN/WAGER");
                var transactionType = Console.ReadLine();

                Console.WriteLine(TestCases.UpdatePlayerBalance(playerIdUpdate, amount, transactionType));
                break;

            default:
                Console.WriteLine("Invalid Selection");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    Console.WriteLine("Press any key to continue");
    Console.ReadLine();
}