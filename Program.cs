using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;


namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TransactionData> allTransactions = ProcessTransactions();
            List<string> allNames = ProcessNames(allTransactions);
            List<UserAccount> alluserAccounts = CreateUserAccounts(allNames, allTransactions);
            SupportBank supportBankExample = new SupportBank(alluserAccounts);

            Console.WriteLine("------------------------");
            Console.WriteLine("Welcome to Support Bank");
            Console.WriteLine("------------------------");
            Console.WriteLine("Choose 1 or 2 : ");
            Console.WriteLine();
            Console.WriteLine("(1) List All");
            Console.WriteLine("(2) List [Account]");
            string userChoice = Console.ReadLine();
            if (userChoice == "1") {
                ListAllTransactions(alluserAccounts);
            } else if (userChoice == "2") {
                Console.WriteLine("Enter the account holder name : ");
                string name = Console.ReadLine();
                ListAccountTransactions(alluserAccounts, name);
            }
        }

        public static List<UserAccount> CreateUserAccounts(List<string> allNames, List<TransactionData> allTransactions)
        {
            List<UserAccount> allUserAccountsList = new List<UserAccount>();

            foreach (string accountname in allNames)
            {
                allUserAccountsList.Add(new UserAccount(accountname, allTransactions));
            }
            return allUserAccountsList;
        }
        public static void ListAccountTransactions(List<UserAccount> alluserAccounts, string accountHolderName)
        {
            var testNameAccount = alluserAccounts.Find(account => account.AccountHolderName == accountHolderName);
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Transaction Details for the account holder : " + accountHolderName);
            Console.WriteLine("--------------------------------------------------------");
            foreach (TransactionData incomingTransaction in testNameAccount.IncomingTransactionHistory)
            {
                Console.Write("Date      : ");
                Console.WriteLine(incomingTransaction.TransactionDate);
                Console.Write("Owed From : ");
                Console.WriteLine(incomingTransaction.TransactionFrom);
                Console.Write("Narrative : ");
                Console.WriteLine(incomingTransaction.TransactionNarrative);
                Console.Write("Amount    : ");
                Console.WriteLine(incomingTransaction.TransactionAmount);
                Console.WriteLine("--------------------------------------------------------");
            }
            foreach (TransactionData outgoingTransaction in testNameAccount.OutgoingTransactionHistory)
            {
                Console.Write("Date      : ");
                Console.WriteLine(outgoingTransaction.TransactionDate);
                Console.Write("Owes To   : ");
                Console.WriteLine(outgoingTransaction.TransactionTo);
                Console.Write("Narrative : ");
                Console.WriteLine(outgoingTransaction.TransactionNarrative);
                Console.Write("Amount    : ");
                Console.WriteLine(outgoingTransaction.TransactionAmount);
                Console.WriteLine("--------------------------------------------------------");
            }
        }

        public static void ListAllTransactions(List<UserAccount> alluserAccounts)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Money Owed/Are Owed for each account holder");
            Console.WriteLine("--------------------------------------------------------");
            foreach (UserAccount userAccount in alluserAccounts)
            {
                Console.Write("Name              : ");
                Console.WriteLine(userAccount.AccountHolderName);
                Console.Write("Amount to receive : ");
                Console.WriteLine(Math.Round(userAccount.BalanceToReceive, 2));
                Console.Write("Amount owed       : ");
                Console.WriteLine(Math.Round(userAccount.BalanceToPay, 2));
                Console.WriteLine("--------------------------------------------------------");
            }
        }



        // Utility Methods
        public static List<TransactionData> ProcessTransactions()
        {
            List<TransactionData> allTransactionsList = new List<TransactionData>();
            string filePath = "./Transactions2014.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    allTransactionsList.Add(new TransactionData(values[0], values[1], values[2], values[3], values[4]));
                }
            }
            return allTransactionsList;
        }

        public static List<string> ProcessNames(List<TransactionData> allTransactions)
        {
            List<string> allNamesList = new List<string>();
            foreach (TransactionData transaction in allTransactions)
            {
                if (!allNamesList.Contains(transaction.TransactionFrom)) allNamesList.Add(transaction.TransactionFrom);
                if (!allNamesList.Contains(transaction.TransactionTo)) allNamesList.Add(transaction.TransactionTo);
            }

            return allNamesList;
        }

    }

}
