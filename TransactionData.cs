using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace SupportBank
{
    public class TransactionData
    {

        [JsonProperty("Date")]
        public DateTime TransactionDate { get; private set; }

        [JsonProperty("FromAccount")]
        public string TransactionFrom { get; private set; }
        [JsonProperty("ToAccount")]
        public string TransactionTo { get; private set; }
        [JsonProperty("Narrative")]
        public string TransactionNarrative { get; private set; }
        [JsonProperty("Amount")]
        public float TransactionAmount { get; private set; }

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