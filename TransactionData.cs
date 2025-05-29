using System;
using System.Globalization;

namespace SupportBank
{
    public class TransactionData
    {
        public DateTime TransactionDate {get; private set;}
        public string TransactionFrom {get; private set;}
        public string TransactionTo {get; private set;}
        public string TransactionNarrative {get; private set;}
        public float TransactionAmount {get; private set;}

        public TransactionData(DateTime transactionDate, string transactionFrom, string transactionTo, string transactionNarrative, float transactionAmount) 
        {
            TransactionDate = transactionDate;
            TransactionFrom = transactionFrom;
            TransactionTo = transactionTo;
            TransactionNarrative = transactionNarrative;
            TransactionAmount = transactionAmount;
        }
    }
}