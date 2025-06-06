﻿using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Globalization;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Linq;



namespace SupportBank
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {

            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Users\RacKel\Training\SupportBank\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, target));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, target));
            LogManager.Configuration = config;


            Logger.Info("Programme started");
            try
            {
                List<TransactionData> allTransactions = CsvProcessor.ProcessTransactions();
                List<string> allNames = CsvProcessor.ProcessNames(allTransactions);
                List<UserAccount> alluserAccounts = CreateUserAccounts(allNames, allTransactions);
                TransactionReport report1 = new TransactionReport(alluserAccounts);

                Console.WriteLine("------------------------");
                Console.WriteLine("Welcome to Support Bank");
                Console.WriteLine("------------------------");
                Console.WriteLine("Choose 1 or 2 : ");
                Console.WriteLine();
                Console.WriteLine("(1) List All");
                Console.WriteLine("(2) List [Account]");
                string userChoice = CheckChoice();
                Logger.Info($"User chose report option {userChoice}");
                if (userChoice == "1")
                {
                    report1.ListAllTransactions();
                    Logger.Info("All transactions printed");
                }
                else if (userChoice == "2")
                {
                    Console.WriteLine("Enter the account holder name:  e.g. Tim L ");
                    string name = EnterName(allNames);
                    report1.ListAccountTransactions(name);
                    Logger.Info($"printed transactions for {name}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            Logger.Info("Programme ended");
        }

        public static List<UserAccount> CreateUserAccounts(List<string> allNames, List<TransactionData> allTransactions)
        {
            List<UserAccount> allUserAccountsList = new List<UserAccount>();

            foreach (string accountname in allNames)
            {
                allUserAccountsList.Add(new UserAccount(accountname, allTransactions));
            }
            return allUserAccountsList;
        }

        // Validation Methods

        public static string EnterName(List<string> allNames)
        {
            bool enteredName = false;
            List<string> lowerCaseNames = allNames.ConvertAll(name => name.ToLower());
            string name = "";
            while (!enteredName)
            {
                name = Console.ReadLine();
                if (lowerCaseNames.Contains(name.ToLower()))
                {
                    enteredName = true;
                    break;
                }
                Console.WriteLine("Please enter a valid name: ");
            }
            return name;
        }

        public static string CheckChoice()
        {
            bool enteredChoice = false;
            string userChoice = "";
            while (!enteredChoice)
            {
                userChoice = Console.ReadLine();
                if (userChoice == "1" || userChoice == "2")
                {
                    enteredChoice = true;
                    break;
                }
                Console.WriteLine("Please enter a valid option: 1 or 2!");

            }
            return userChoice;

        }

    }
}
