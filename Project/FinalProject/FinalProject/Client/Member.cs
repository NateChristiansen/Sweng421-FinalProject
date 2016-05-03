using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FinalProject
{
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

        public Member(string first, string last, string user, string pass, decimal wallet)
        {
            this.Username = user;
            this.LastName = last;
            this.Password = pass;
            this.FirstName = first;
            this.Wallet = wallet;
        }

        public Member(string userName, string passWord, decimal wallet)
        {
            this.Username = userName;
            this.Password = passWord;
            this.Wallet = wallet;
        }

        // creates the user's registration file
        public void CreateUserFile()
        {
            string fileName = FirstName + LastName + ".txt";
            var directoryPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            var fullPath = Path.Combine(directoryPath, "RegisteredUsers");
            var filePath = Path.Combine(fullPath, fileName);
            if (!File.Exists(filePath))
            {
                TextWriter tw = new StreamWriter(filePath, true);
                tw.WriteLine(Username);
                tw.WriteLine(Password);
                tw.WriteLine(Wallet);
                tw.Close();

            }
            else
            {
                MessageBox.Show("Error creating file in database. Please try again.");
            }

            
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

        public void Notify(string title)
        {
            throw new NotImplementedException();
        }

    }
}