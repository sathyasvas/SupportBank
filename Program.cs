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
            TransactionReport report1 = new TransactionReport(alluserAccounts);

            Console.WriteLine("------------------------");
            Console.WriteLine("Welcome to Support Bank");
            Console.WriteLine("------------------------");
            Console.WriteLine("Choose 1 or 2 : ");
            Console.WriteLine();
            Console.WriteLine("(1) List All");
            Console.WriteLine("(2) List [Account]");
            string userChoice = Console.ReadLine();
            if (userChoice == "1") {
                report1.ListAllTransactions();
            } else if (userChoice == "2") {
                Console.WriteLine("Enter the account holder name : ");
                string name = Console.ReadLine();
                report1.ListAccountTransactions(name);
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
