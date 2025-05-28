using System;
using System.IO;
using static SupportBank.TransactionData;
namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TransactionData> allTransactions = new List<TransactionData>();

            string filePath = "./Transactions2014.csv";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    allTransactions.Add(new TransactionData(values[0], values[1], values[2], values[3], values[4]));
                }
            }

            List<string> Names = new List<string> ();
        foreach(TransactionData transaction in allTransactions)
            {        
                // Console.WriteLine(transaction.TransactionDate);
                // Console.WriteLine(transaction.TransactionFrom);
                // Console.WriteLine(transaction.TransactionTo);
                // Console.WriteLine(transaction.TransactionNarrative);
                // Console.WriteLine(transaction.TransactionAmount);

                if(!Names.Contains(transaction.TransactionFrom)){
                    Names.Add(transaction.TransactionFrom);
                }
                if(!Names.Contains(transaction.TransactionTo)){
                    Names.Add(transaction.TransactionTo);
                }
            }





        }
    }
    
}
