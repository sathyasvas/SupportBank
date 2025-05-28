using System;

namespace SupportBank
{
    public class TransactionData
    {
        public string TransactionDate {get; private set;}
        public string TransactionFrom {get; private set;}
        public string TransactionTo {get; private set;}
        public string TransactionNarrative {get; private set;}
        public string TransactionAmount {get; private set;}

        public TransactionData(string transactionDate, string transactionFrom, string transactionTo, string transactionNarrative, string transactionAmount) 
        {
            TransactionDate = transactionDate;
            TransactionFrom = transactionFrom;
            TransactionTo = transactionTo;
            TransactionNarrative = transactionNarrative;
            TransactionAmount = transactionAmount;
        }
    }
}