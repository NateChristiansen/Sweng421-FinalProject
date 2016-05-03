using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace FinalProject
{
    [Serializable]
    public class Member
    {
        public decimal Wallet { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> SubscriberList = new List<string>();
        public List<IBook> OwnedBooks = new List<IBook>();
        public List<Notification> Notifications = new List<Notification>(); 

        public Member(string first, string last, string user, string pass, decimal wallet)
        {
            Username = user;
            LastName = last;
            Password = pass;
            FirstName = first;
            Wallet = wallet;
        }

        public Member(string userName, string passWord, decimal wallet)
        {
            Username = userName;
            Password = passWord;
            Wallet = wallet;
        }

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

        public void Notify(IBook book)
        {
            Notifications.Add(new Notification
            {
<<<<<<< HEAD
                Text = string.Format("{0} by {1} is now in stock!", book.Title, book.Author),
                Time = DateTime.Now
=======
                Text = $"{book.Title} by {book.Author} is now in stock!",
                Time = DateTime.Now,
                Book = book
>>>>>>> d48305cc9966cebb2376615a230182804ecbc2c6
            });
        }

        [Serializable]
        public class Notification
        {
            public string Text { get; set; } 
            public DateTime Time { get; set; }
            public IBook Book { get; set; }
        }
    }
}