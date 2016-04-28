using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class Member
    {
        protected decimal Wallet;
        protected string Username;
        protected string FirstName;
        protected string LastName;
        protected string Email;
        protected List<string> SubscriberList = new List<string>();

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

        public void Notify(string title)
        {
            throw new NotImplementedException();
        }
        private Member()
        {
        }

        public static Member MapFromDb(User user)
        {
            var mem = new Member
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Wallet = user.Wallet,
                SubscriberList = user.Subs != string.Empty ? user.Subs.Split('~').ToList() : new List<string>()
            };
            return mem;
        }

        public void UpdateUser(User user)
        {
            user.Wallet = Wallet;
            user.Subs = string.Join("~", SubscriberList);
        }
    }
}