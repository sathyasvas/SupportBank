using System;

namespace SupportBank 
{
    public class SupportBank {
        public List<UserAccount> UserAccounts {get; private set;}

        public SupportBank(List<UserAccount> alluserAccounts)
        {
            this.UserAccounts = alluserAccounts;
        }

        // More methods

    }

}