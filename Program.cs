using System;
using System.IO;


namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TransactionData> allTransactions = ProcessTransactions();
            List<string> allNames = ProcessNames(allTransactions);

            List<UserAccount> alluserAccounts = new List<UserAccount>();
         
            // SupportBank supportBankExample = new SupportBank(allNames, allTransactions);


            foreach (string name in allNames)
            {
                alluserAccounts.Add(new UserAccount(name, allTransactions));
            }

            var testNameAccount = alluserAccounts.Find(account => account.AccountHolderName == "Ben B");
            Console.WriteLine(testNameAccount.AccountHolderName);
            Console.WriteLine(testNameAccount.BalanceToReceive);
            Console.WriteLine(testNameAccount.BalanceToPay);

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
