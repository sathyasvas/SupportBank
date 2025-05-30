using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Globalization;
using NLog;
using NLog.Config;
using NLog.Targets;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Reflection.Metadata;
using System.Xml;


namespace SupportBank
{
    public class FileProcessor
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static List<TransactionData> ProcessTransactions(string filePath)
        {
            List<TransactionData> allTransactionsList = new List<TransactionData>();
            if (filePath.Contains("csv"))
            {
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
            else if (filePath.Contains("json"))
            {
                string json = File.ReadAllText(filePath);
                if (json != null)
                {
                    var tempTransactionsList = JsonConvert.DeserializeObject<List<TransactionData>>(json);
                    if (tempTransactionsList != null)
                    {
                        allTransactionsList = tempTransactionsList;
                        Logger.Info("The json file has been read");
                        return allTransactionsList;
                    }
                    else
                    {
                        Logger.Info("JSON content could not be deserialized into transactions.");
                    }
                }
                else
                {
                    Logger.Info("JSON file is empty or null.");
                }
            }
            else if (filePath.Contains("xml"))
            {
                XDocument doc = XDocument.Load(filePath);
                var transactions = doc.Descendants("SupportTransaction");

                foreach (var transaction in transactions)
                {
                    DateTime convertedDate = new DateTime(1900, 1, 1).AddDays(double.Parse(transaction.Attribute("Date").Value) - 2);
                    string from = transaction.Element("Parties").Element("From").Value;
                    string to = transaction.Element("Parties").Element("To").Value;
                    string narrative = transaction.Element("Description").Value;
                    float parsedAmount;

                    if (float.TryParse(transaction.Element("Value").Value, out parsedAmount))
                    {
                        allTransactionsList.Add(new TransactionData(convertedDate, from, to, narrative, parsedAmount));
                    }
                    else
                    {
                        Logger.Info("This information was not processed as a transaction: " + transaction);
                    }
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
