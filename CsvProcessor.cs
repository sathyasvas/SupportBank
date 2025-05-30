using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Globalization;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    public class CsvProcessor
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();



        public static List<TransactionData> ProcessTransactions()
        {
            List<TransactionData> allTransactionsList = new List<TransactionData>();
            string filePath = "./DodgyTransactions2015.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    DateTime parsedDate;
                    float parsedAmount;
                    if (DateTime.TryParseExact(values[0], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) && float.TryParse(values[4], out parsedAmount))
                    {
                        allTransactionsList.Add(new TransactionData(parsedDate, values[1], values[2], values[3], parsedAmount));
                    }
                    else
                    {
                        Logger.Info("This information was not processed as a transaction: " + line);
                    }
                }

            }
            Logger.Info("The csv file has been read");
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
