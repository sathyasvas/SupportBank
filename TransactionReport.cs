using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank 
{
    public class TransactionReport
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public List<UserAccount> UserAccounts { get; private set; }

        public TransactionReport(List<UserAccount> alluserAccounts)
        {
            this.UserAccounts = alluserAccounts;
        }


        // More methods
        
        public void ListAccountTransactions(string accountHolderName)
        {
            var testNameAccount = UserAccounts.Find(account => account.AccountHolderName.ToLower() == accountHolderName.ToLower());
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Transaction Details for the account holder : " + testNameAccount.AccountHolderName);
            Console.WriteLine("--------------------------------------------------------");
            foreach (TransactionData incomingTransaction in testNameAccount.IncomingTransactionHistory)
            {
                Console.Write("Date      : ");
                Console.WriteLine(incomingTransaction.TransactionDate.ToShortDateString());
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
                Console.WriteLine(outgoingTransaction.TransactionDate.ToShortDateString());
                Console.Write("Owes To   : ");
                Console.WriteLine(outgoingTransaction.TransactionTo);
                Console.Write("Narrative : ");
                Console.WriteLine(outgoingTransaction.TransactionNarrative);
                Console.Write("Amount    : ");
                Console.WriteLine(outgoingTransaction.TransactionAmount);
                Console.WriteLine("--------------------------------------------------------");
            }
        }

        public void ListAllTransactions()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Money Owed/Are Owed for each account holder");
            Console.WriteLine("--------------------------------------------------------");
            foreach (UserAccount userAccount in UserAccounts)
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



    }

}