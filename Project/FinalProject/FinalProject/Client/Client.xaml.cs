﻿using System.Collections.Generic;
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
            LoginButton.Content = "Logout";
        }

        public void Logout()
        {
            _user = null;
            LoginButton.Content = "Login";
        }

        private void LoginButton_Clicked(object sender, MouseButtonEventArgs e)
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
