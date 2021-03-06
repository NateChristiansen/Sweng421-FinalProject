﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.VisualBasic;

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
        private List<Member> _users; 

        public Client(Store s)
        {
            InitializeComponent();
            _store = s;
            LoadUsers();
            InitUi();
            PopulateStore();
            Show();
        }

        private void LoadUsers()
        {
            try
            {
                using (var stream = File.Open("RegisteredUsers.bin", FileMode.Open))
                {
                    var bformatter = new BinaryFormatter();
                    _users = (List<Member>) bformatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                _users = new List<Member>();
            }
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

            NotificationGrid.Columns.Clear();
            var notif = new DataGridTextColumn {Header = "Notification", IsReadOnly = true, Width = NotificationGrid.Width, Binding = new Binding("Text")};
            NotificationGrid.Columns.Add(notif);
        }

        private void TabItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = ((Label) sender).Content.ToString();
            switch (text)
            {
                case "Cart":
                    CartGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                    CartGrid.ItemsSource = _cart;
                    decimal d = 0;
                    _cart.ForEach(c => d += c.Price);
                    TotalLabel.Content = "$" + d;
                    break;
                case "Browse":
                    PopulateStore();
                    break;
                case "Notifications":
                    NotificationGrid.ClearValue(ItemsControl.ItemsSourceProperty);
                    if (_user != null)
                        NotificationGrid.ItemsSource = _user.Notifications;
                    break;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var query = SearchBox.Text;
            SearchGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            SearchGrid.ItemsSource = _filter.Apply(SearchBox.Text, _store.Search(query));
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
            BrowseGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            BrowseGrid.ItemsSource = _store.GetBooks();
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
            if (CheckStore(book))
                _cart.Add(book);
        }

        private bool CheckStore(IBook book)
        {
            if (_store.InStock(book))
                return true;
            var result =
                MessageBox.Show("This item is not currently in stock, would you like to subscribe to be notified when it becomes available?", "Out of Stock", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return false;
            if (_user != null)
            {
                _store.Subscribe(_user, book);
                _user.SubscriberList.Add(book);
            }
            else
                MessageBox.Show("Please log in before subscribing.", "Not Logged In", MessageBoxButton.OK);
            return false;
        }

        public void Register(LogRegWin lrw)
        {
            var first = lrw.RegNameBox.Text;
            var last = lrw.RegLastNameBox.Text;
            var userName = lrw.RegUserBox.Text;
            var passWord = lrw.RegPassBox.Text;
            var check = _users.FirstOrDefault(u => u.Username.Equals(userName));

            if (check == null)
            {
                if (first.Equals("") || last.Equals("") || userName.Equals("") || passWord.Equals("")) return;
                _user = new Member(first, last, userName, passWord, (decimal) 50.00);
                _users.Add(_user);
                LoggedInLabel.Content = _user.Username + " - $" + _user.Wallet;
                LoginButton.Content = "Logout";
            }
            else
            {
                MessageBox.Show("User already exists. Please enter your own credentials.");
            }
        }

        public void Login(LogRegWin lrw)
        {
            var user = lrw.UserNameBox.Text;
            var pass = lrw.PasswordBox.Text;
            _user = _users.FirstOrDefault(u => u.Username.Equals(user) && u.Password.Equals(pass));
            if (_user == null) return;
            _user.SubscriberList.ForEach(s =>
            {
                if(_store.InStock(s))
                    _user.Notify(s);
                else
                    _store.Subscribe(_user, s);
            });
            LoggedInLabel.Content = _user.Username + " - $" + _user.Wallet;
            LoginButton.Content = "Logout";
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
                var loginWin = new LogRegWin(this);
                loginWin.ShowDialog();
            } 
            else
            {
                Logout();
            }
                
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
            _cart.Remove(book);
            CartGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            CartGrid.ItemsSource = _cart;
            decimal d = 0;
            _cart.ForEach(c => d += c.Price);
            TotalLabel.Content = "$" + d;
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCart();
        }

        private void CartSelected(object sender, SelectionChangedEventArgs e)
        {
            var selected = (IBook) CartGrid.SelectedItem;
            if (selected == null) return;
            CartDescriptionText.Text = selected.Summary;
            CartPreview.Source = selected.Cover;
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                var loginWin = new LogRegWin(this);
                loginWin.ShowDialog();
                return;
            }
            decimal d = 0;
            _cart.ForEach(c => d += c.Price);
            if (d > _user.Wallet)
            {
                MessageBox.Show("You do not have enough funds to purchase these books.", "Not Enough Funds", MessageBoxButton.OK);
                return;
            }
            _cart.ForEach(b =>
            {
                if (!CheckStore(b))
                    _cart.Remove(b);
            });
            _store.PurchaseBooks(_cart, _user);
            LoggedInLabel.Content = _user.Username + " - $" + _user.Wallet;
            ClearCart();
        }

        private void ClearCart()
        {
            _cart.Clear();
            CartGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            CartGrid.ItemsSource = _cart;
            decimal d = 0;
            _cart.ForEach(c => d += c.Price);
            TotalLabel.Content = "$" + d;
        }

        private void SaveUsers(object sender, CancelEventArgs e)
        {
            using (Stream stream = File.Open("RegisteredUsers.bin", FileMode.OpenOrCreate))
            {
                var bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, _users);
            }
        }

        private void ClearAllNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null) return;
            _user.Notifications.Clear();
            NotificationGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            NotificationGrid.ItemsSource = _user.Notifications;
        }

        private void ClearSelectedNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null) return;
            _user.Notifications.Remove((Member.Notification) NotificationGrid.SelectedItem);
            NotificationGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            NotificationGrid.ItemsSource = _user.Notifications;
        }

        private void AddNotificationToCart_Click(object sender, RoutedEventArgs e)
        {
            var notification = ((Member.Notification) NotificationGrid.SelectedItem);
            _cart.Add(notification.Book);
        }

        private void Client_OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
