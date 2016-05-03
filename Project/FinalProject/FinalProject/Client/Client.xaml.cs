﻿using System;
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
using RadioButton = System.Windows.Controls.RadioButton;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ClientUI.xaml
    /// </summary>
    public partial class Client : Window
    {
        private readonly Store _store;
        private readonly List<IBook> _cart = new List<IBook>();
        private Member _user;
        private ISearchFilter _filter = new TitleFilter(null);

        public Client(Store s)
        {
            InitializeComponent();
            _store = s;
            InitUi();
            PopulateStore();
            Show();
        }

        private void InitUi()
        {
            BrowseGrid.Columns.Clear();
            var title = new DataGridTextColumn { Header = "Title", Binding = new Binding("Title"), IsReadOnly = true, Width = BrowseGrid.Width / 5 };
            var author = new DataGridTextColumn { Header = "Author", Binding = new Binding("Author"), IsReadOnly = true, Width = BrowseGrid.Width / 5 };
            var genre = new DataGridTextColumn { Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true, Width = BrowseGrid.Width / 5 };
            var publisher = new DataGridTextColumn { Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true, Width = BrowseGrid.Width / 5 };
            var price = new DataGridTextColumn { Header = "Price", Binding = new Binding("Price"), IsReadOnly = true, Width = BrowseGrid.Width / 5 };
            BrowseGrid.Columns.Add(title);
            BrowseGrid.Columns.Add(author);
            BrowseGrid.Columns.Add(genre);
            BrowseGrid.Columns.Add(publisher);
            BrowseGrid.Columns.Add(price);

            SearchGrid.Columns.Clear();
            var title1 = new DataGridTextColumn { Header = "Title", Binding = new Binding("Title"), IsReadOnly = true, Width = SearchGrid.Width / 5 };
            var author1 = new DataGridTextColumn { Header = "Author", Binding = new Binding("Author"), IsReadOnly = true, Width = SearchGrid.Width / 5 };
            var genre1 = new DataGridTextColumn { Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true, Width = SearchGrid.Width / 5 };
            var publisher1 = new DataGridTextColumn { Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true, Width = SearchGrid.Width / 5 };
            var price1 = new DataGridTextColumn { Header = "Price", Binding = new Binding("Price"), IsReadOnly = true, Width = SearchGrid.Width / 5 };
            SearchGrid.Columns.Add(title1);
            SearchGrid.Columns.Add(author1);
            SearchGrid.Columns.Add(genre1);
            SearchGrid.Columns.Add(publisher1);
            SearchGrid.Columns.Add(price1);

            CartGrid.Columns.Clear();
            var title2 = new DataGridTextColumn { Header = "Title", Binding = new Binding("Title"), IsReadOnly = true, Width = CartGrid.Width / 5 };
            var author2 = new DataGridTextColumn { Header = "Author", Binding = new Binding("Author"), IsReadOnly = true, Width = CartGrid.Width / 5 };
            var genre2 = new DataGridTextColumn { Header = "Genre", Binding = new Binding("Genre"), IsReadOnly = true, Width = CartGrid.Width / 5 };
            var publisher2 = new DataGridTextColumn { Header = "Publisher", Binding = new Binding("Publisher"), IsReadOnly = true, Width = CartGrid.Width / 5 };
            var price2 = new DataGridTextColumn { Header = "Price", Binding = new Binding("Price"), IsReadOnly = true, Width = CartGrid.Width / 5 };
            CartGrid.Columns.Add(title2);
            CartGrid.Columns.Add(author2);
            CartGrid.Columns.Add(genre2);
            CartGrid.Columns.Add(publisher2);
            CartGrid.Columns.Add(price2);
        }

        private void TabItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = ((Label) sender).Content.ToString();
            switch (text)
            {
                case "Cart":
                    //CartGrid.Items.Clear();
                    CartGrid.ItemsSource = _cart;
                    decimal d = 0;
                    _cart.ForEach(c => d += c.Price);
                    TotalLabel.Content = "$" + d;
                    break;
                case "Browse":
                    PopulateStore();
                    break;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBox.Text;
            SearchGrid.Items.Clear();
            _filter.Apply(SearchBox.Text, _store.Search(query)).ForEach(b => SearchGrid.Items.Add(b));
        }

        private void AddSearchToCart_Click(object sender, RoutedEventArgs e)
        {
            var selected = (IBook)SearchGrid.SelectedItem;
            if (selected == null) return;
            AddToCart(selected);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateStore();
        }

        private void PopulateStore()
        {
            BrowseGrid.Items.Clear();
            _store.GetBooks().ForEach(b => BrowseGrid.Items.Add(b));
        }

        private void BrowseSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var selected = (IBook) BrowseGrid.SelectedItem;
            if (selected == null) return;
            BrowseDescriptionText.Text = selected.Summary;
            BrowseImage.Source = selected.Cover;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (IBook) BrowseGrid.SelectedItem;
            if (selected == null) return;
            AddToCart(selected);
        }

        private void AddToCart(IBook book)
        {
            if (_store.InStock(book.Title))
                _cart.Add(book);
            else
            {
                var result =
                    Interaction.MsgBox(
                        "This item is not currently in stock, would you like to subscribe to be notified when it becomes available?",
                        MsgBoxStyle.YesNo, "Out of Stock");
                if (result == MsgBoxResult.No) return;
                if (_user != null)
                    _store.Subscribe(_user, book.Title);
                else
                    Interaction.MsgBox("Please log in before subscribing.", MsgBoxStyle.OkOnly, "Not Logged In");
            }
        }

        public void Login()
        {
            var dr = (DialogResult) MessageBox.Show("Click yes to register, no to login", "Register or Login",
                MessageBoxButton.YesNo);

            // perform registration
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                var first = Interaction.InputBox("Please enter your first name:",
                    "First", "", -1, -1);

                var last = Interaction.InputBox("Please enter your last name:",
                    "Last", "", -1, -1);

                var userName = Interaction.InputBox("Please enter your username:",
                    "Username", "", -1, -1);

                var passWord = Interaction.InputBox("Please enter your password:",
                    "Username", "", -1, -1);


                if (first.Equals("") || last.Equals("") || userName.Equals("") || passWord.Equals("")) return;
                _user = new Member(first, last, userName, passWord, (decimal)50.00);
                _user.CreateUserFile();

                LoggedInLabel.Content = "Logged in as: " + _user.GetUsername();

                LoginButton.Content = "Logout";
            }
            else
            {
                var userName = Interaction.InputBox("Please enter your username:",
                    "Username", "", -1, -1);

                var passWord = Interaction.InputBox("Please enter your password:",
                    "Username", "", -1, -1);

                var directoryPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                directoryPath = Path.Combine(directoryPath, "RegisteredUsers");

                var userFound = false;

                foreach (var file in Directory.EnumerateFiles(directoryPath, "*.txt"))
                {
                    var fileLines = File.ReadAllLines(file);

                    if (!fileLines[0].Equals(userName) || !fileLines[1].Equals(passWord)) continue;
                    var wallet = decimal.Parse(fileLines[2]);
                    _user = new Member(userName, passWord, wallet);

                    LoggedInLabel.Content = "Logged in as: " + _user.GetUsername();

                    userFound = true;

                    LoginButton.Content = "Logout";
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
                Login();
            else
                Logout();
        }

        private void FilterButtonChecked(object sender, RoutedEventArgs e)
        {
            var filter = (string) ((RadioButton) sender).Content;
            switch (filter)
            {
                case "Title":
                    _filter = new TitleFilter(_filter);
                    break;
                case "Author":
                    _filter = new AuthorFilter(_filter);
                    break;
                case "Publisher":
                    _filter = new PublisherFilter(_filter);
                    break;
                case "Genre":
                    _filter = new GenreFilter(_filter);
                    break;
            }
        }

        private void SearchSelected(object sender, SelectionChangedEventArgs e)
        {
            var selected = (IBook) SearchGrid.SelectedItem;
            if (selected == null) return;
            SearchDescriptionText.Text = selected.Summary;
            SearchResultImage.Source = selected.Cover;
        }

        private void ClearSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var book = (IBook) CartGrid.SelectedItem;
            if (book == null) return;
            CartGrid.Items.Remove(book);
            _cart.Remove(book);
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            CartGrid.Items.Clear();
            _cart.Clear();
        }

        private void CartSelected(object sender, SelectionChangedEventArgs e)
        {
            var selected = (IBook) CartGrid.SelectedItem;
            if (selected == null) return;
            CartDescriptionText.Text = selected.Summary;
            CartPreview.Source = selected.Cover;
        }
    }
}
