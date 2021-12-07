using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BankingApp.Logic.CurrencyFreaksApi;
using BankingApp.Logic.Database;
using BankingApp.Logic.Model;
using BankingApp.Logic.MOM;

namespace BankingApp.Controller
{
    /// <summary>
    /// Logique d'interaction pour ClientView.xaml
    /// </summary>
    public partial class ClientController
    {
        private Client _client;
        private ClientModel _model;
        private Currency _currency;
        public ClientController(Client c)
        {
            _client = c;
            _model = new ClientModel(new ClientDBAccess());
            _currency = new Currency();
            _currency.LoadCurrencies();
            InitializeComponent();
            InitializeWindowProperty(_client);
            BindingComboBox();
            PinTextBox.IsEnabled = false;
            SavePinButton.IsEnabled = false;
        }

        private void Modify_Pin_Click(object sender, RoutedEventArgs e)
        {
            PinTextBox.IsEnabled = true;
            SavePinButton.IsEnabled = true;
        }

        private void Save_Pin_Click(object sender, RoutedEventArgs e)
        {
            _client.Pin = PinTextBox.Text;
            _model.DbClient.UpdateClient(_client);
            SavePinButton.IsEnabled = false;
            PinTextBox.IsEnabled = false;
        }

        private void Add_Money_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeAmountTextBox.Text == String.Empty) return;
            double amount = Convert.ToDouble(ChangeAmountTextBox.Text, new NumberFormatInfo());
            if (amount <= 0)
            {
                MessageBox.Show("Please insert a correct amount to add");
                return;
            }
            _client.Amount += amount;
            _model.DbClient.UpdateClient(_client);
            AmountValue.Content = _client.Amount.ToString(CultureInfo.InvariantCulture);
            ChangeAmountTextBox.Text = String.Empty;
        }

        private void Get_Money_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeAmountTextBox.Text == String.Empty) return;
            double amount = Convert.ToDouble(ChangeAmountTextBox.Text, new NumberFormatInfo());
            if (amount > _client.Amount)
            {
                MessageBox.Show("Operation fails. You can't retrieve this amount from your account");
                return;
            }

            _client.Amount -= amount;
            _model.DbClient.UpdateClient(_client);
            AmountValue.Content = _client.Amount.ToString(CultureInfo.InvariantCulture);
            ChangeAmountTextBox.Text = String.Empty;
        }
        
        private void Send_Message_Click(object sender, RoutedEventArgs e)
        {
            if (MessageContentTextBox.Text == String.Empty)
            {
                MessageBox.Show("You can't send an empty message");
                return;
            } 
            var isSended = Producer.SendMessage("Id : " + _client.Id + " FirstName : " + _client.FirstName + " LastName : "
                                                + _client.LastName + " Comments : " + MessageContentTextBox.Text);
           if (isSended) MessageBox.Show("Message send Successfully");
           MessageContentTextBox.Text = String.Empty;
        }

        private void Refresh_Message_Click(object sender, RoutedEventArgs e)
        {
            //A faire s'il me reste du temps
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            _client.IsBlocked = false;
            new ClientModel(new ClientDBAccess()).DbClient.UpdateClient(_client);
            Close();
        }

        private void InitializeWindowProperty(Client c)
        {
            IdValue.Content = c.Id.ToString();
            FirstNameValue.Content = c.FirstName;
            LastNameValue.Content = c.LastName;
            AmountValue.Content = c.Amount.ToString(CultureInfo.InvariantCulture);
            PinTextBox.Text = c.Pin;
        }

        private void BindingComboBox()
        { 
            CurrencyComboBox.ItemsSource = CountryCurrency.CountryCurrenciesFactory();
        }
        private void Select_Currency_Click(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb != null)
            {
                CountryCurrency cc = cmb.SelectedItem as CountryCurrency;
                if (cc != null)
                {
                    AmountInCurrency.Content = _currency.GetAmountIn(_client.Amount, cc.Name);
                    CurrentCurrency.Content = "(" + cc.Name + ")";
                }
            }
        }

        //Associe chaque pays à son symbol
        public class CountryCurrency
        {
            public string Img { get; set; }
            public string Name { get; set; }

            public static List<CountryCurrency> CountryCurrenciesFactory()
            {
                return new List<CountryCurrency>()
                {
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/eur.png",
                        Name = Currency.EURO
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/bch.png",
                        Name = Currency.BITCOIN_CASH
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/aoa.png",
                        Name = Currency.ANGOLA_KWANZA
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/cop.png",
                        Name = Currency.COLOMBIAN_PESO
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/cad.png",
                        Name = Currency.CANADIAN_DOLLAR
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/aed.png",
                        Name = Currency.UAE_DIRHAM
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/cnh.png",
                        Name = Currency.CHINESE_YUAN_RENMINBI
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/eth.png",
                        Name = Currency.ETHEREUM
                    },
                    new CountryCurrency()
                    {
                        Img = "https://currencyfreaks.com/photos/flags/rub.png",
                        Name = Currency.RUSSIAN_RUBLE
                    }

                };
            }
        }
    }
}
