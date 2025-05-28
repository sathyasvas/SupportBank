using System;

namespace SupportBank
{
    
    public class UserAccount
    {
    public string AccountHolderName {get; private set;}
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
                {
                    OutgoingTransactionHistory.Add(transaction);
                    BalanceToPay += float.Parse(transaction.TransactionAmount);
                }
                if (transaction.TransactionTo == accountHolderName)
                {
                    IncomingTransactionHistory.Add(transaction);
                    BalanceToReceive += float.Parse(transaction.TransactionAmount);
                }
                }
            }
        
    }
        
    }
}