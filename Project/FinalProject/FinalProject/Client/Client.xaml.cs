using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Binding = System.Windows.Data.Binding;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ClientUI.xaml
    /// </summary>
    public partial class Client : Window
    {
        private readonly Store _store;
        private readonly List<string> _cart = new List<string>();
        private Member _user;

        public Client(Store s)
        {
            InitializeComponent();
            _store = s;
            PopulateStore();
            Show();
        }

        private void InitUi()
        {
        }

        private void TabItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = ((Label) sender).Content.ToString();
            switch (text)
            {
                case "Cart":
                    //CartList.Items.Clear();
                    CartList.ItemsSource = _cart;
                    break;
                case "Browse":
                    PopulateStore();
                    break;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBox.Text;
            SearchResultList.Items.Clear();
            _store.Search(query).ForEach(b => SearchResultList.Items.Add(b.Title));
        }

        private void GoToStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResultList.SelectedItems.Count == 0) return;
            var x = SearchResultList.SelectedItems[0] as string;

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateStore();
        }

        private void PopulateStore()
        {
            BrowseGrid.Columns.Clear();
            var title = new DataGridTextColumn {Header = "Title", Binding = new Binding("Title"), IsReadOnly = true};
            var author = new DataGridTextColumn {Header = "Author", Binding = new Binding("Author"), IsReadOnly = true};
            var genre = new DataGridTextColumn {Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true};
            var publisher = new DataGridTextColumn {Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true};
            var price = new DataGridTextColumn {Header = "Price", Binding = new Binding("Price"), IsReadOnly = true};
            BrowseGrid.Columns.Add(title);
            BrowseGrid.Columns.Add(author);
            BrowseGrid.Columns.Add(genre);
            BrowseGrid.Columns.Add(publisher);
            BrowseGrid.Columns.Add(price);

            foreach (IBook book in _store.GetBooks())
            {
                if (!BrowseGrid.Items.Contains(book))
                {
                    BrowseGrid.Items.Add(book);
                }
            }
        }

        private void BrowseSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var selected = (IBook) selectionChangedEventArgs.AddedItems[0];
            BrowseDescriptionText.Text = selected.Summary;
            BrowseImage.Source = selected.Cover;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (IBook) BrowseGrid.SelectedItems[0];
            if (_store.InStock(selected.Title))
                _cart.Add(selected.Title);
            else
            {
                var result =
                    Interaction.MsgBox(
                        "This item is not currently in stock, would you like to subscribe to be notified when it becomes available?",
                        MsgBoxStyle.YesNo, "Out of Stock");
                if (result == MsgBoxResult.No) return;
                if (_user != null)
                    _store.Subscribe(_user, selected.Title);
                else
                    Interaction.MsgBox("Please log in before subscribing.", MsgBoxStyle.OkOnly, "Not Logged In");
            }
        }

        public void Login()
        {
            DialogResult dr = (DialogResult) MessageBox.Show("Click yes to register, no to login", "Register or Login",
                MessageBoxButton.YesNo);

            // perform registration
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                string first = Interaction.InputBox("Please enter your first name:",
                    "First", "", -1, -1);

                string last = Interaction.InputBox("Please enter your last name:",
                    "Last", "", -1, -1);

                string userName = Interaction.InputBox("Please enter your username:",
                    "Username", "", -1, -1);

                string passWord = Interaction.InputBox("Please enter your password:",
                    "Username", "", -1, -1);


                if (!first.Equals("") && !last.Equals("") && !userName.Equals("") && !passWord.Equals(""))
                {
                    _user = new Member(first, last, userName, passWord, (decimal)50.00);
                    _user.CreateUserFile();

                    LoggedInLabel.Content = "Logged in as: " + _user.GetUsername();

                    LoginButton.Content = "Logout";
                }

            }
            else
            {
                string userName = Interaction.InputBox("Please enter your username:", 
                    "Username", "", -1, -1);

                string passWord = Interaction.InputBox("Please enter your password:",
                    "Username", "", -1, -1);

                var directoryPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                directoryPath = Path.Combine(directoryPath, "RegisteredUsers");

                bool userFound = false;

                foreach (string file in Directory.EnumerateFiles(directoryPath, "*.txt"))
                {
                    string[] fileLines = File.ReadAllLines(file);

                    if (fileLines[0].Equals(userName) && fileLines[1].Equals(passWord))
                    {
                        decimal wallet = Decimal.Parse(fileLines[2]);
                        _user = new Member(userName, passWord, wallet);

                        LoggedInLabel.Content = "Logged in as: " + _user.GetUsername();

                        userFound = true;

                        LoginButton.Content = "Logout";
                    }
                }

                if (!userFound)
                {
                    MessageBox.Show("User does not exist. Please check your credentials.");
                }

            }

            
        }

        public void Logout()
        {
            _user = null;

            LoggedInLabel.Content = "";

            LoginButton.Content = "Login";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                Login();
            }
            else
            {
                Logout();
            }
        }
    }
}
