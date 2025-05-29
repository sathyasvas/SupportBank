using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    
    public class UserAccount
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    public string AccountHolderName { get; private set; }
    public float BalanceToReceive {get; private set;}
    public float BalanceToPay {get; private set;}
    public List<TransactionData> OutgoingTransactionHistory {get; private set;}
    public List<TransactionData> IncomingTransactionHistory {get; private set;}

    public UserAccount(string accountHolderName, List<TransactionData> allTransactions)
    {
        
        AccountHolderName = accountHolderName;
        OutgoingTransactionHistory = new List<TransactionData>();
        IncomingTransactionHistory = new List<TransactionData>();
        
        foreach (TransactionData transaction in allTransactions)
            {
                if (transaction != null) {
                    if (transaction.TransactionFrom == accountHolderName)
                        try
                        {
                            OutgoingTransactionHistory.Add(transaction);
                            BalanceToPay += transaction.TransactionAmount;
                        }
                        catch (System.FormatException ex)
                        {
                            Logger.Error(ex.Message);
                        }
                if (transaction.TransactionTo == accountHolderName)
                    {
                        IncomingTransactionHistory.Add(transaction);
                        BalanceToReceive += transaction.TransactionAmount;
                    }
                }
            }
        
    }
        
    }
}