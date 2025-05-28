using System;

namespace SupportBank 
{
    public class SupportBank {
        public List<UserAccount> UserAccounts {get; private set;}

        public SupportBank(List<string> allNames, List<TransactionData> allTransactions)
        {
            UserAccounts = new List<UserAccount>();
            List<UserAccount> userLists = new List<UserAccount>();
            
            foreach (string name in allNames)
            {
                userLists.Add(new UserAccount(name, allTransactions));
            }
            UserAccounts = userLists;
        }

    }

}