using System.Collections.Generic;

namespace FinalProject
{
    public class AbstractMember : IMember
    {
        protected decimal Wallet;
        protected string Username;
        protected string FirstName;
        protected string LastName;
        protected string Email;
        protected List<string> SubscriberList; 
        public decimal GetWallet()
        {
            return Wallet;
        }

        public string GetUsername()
        {
            return Username;
        }

        public string GetFirstName()
        {
            return FirstName;
        }

        public string GetLastName()
        {
            return LastName;
        }

        public string GetEmail()
        {
            return Email;
        }

        public List<string> GetSubs()
        {
            return SubscriberList;
        }
    }
}