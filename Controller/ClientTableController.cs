using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using BankingApp.Logic.Database;
using BankingApp.Logic.Model;

namespace BankingApp.Controller
{
    /// <summary>
    /// Logique d'interaction pour ClientTableView.xaml
    /// </summary>
    public partial class ClientTableController
    {
    private readonly ClientModel _model;

    public ClientTableController()
    {
        _model = new ClientModel(new ClientDBAccess());
        InitializeComponent();
        DataGrid.DataContext = this;
    }

    private void Button_Add_Client_Click(object sender, RoutedEventArgs e)
    {
        if (!IsEveryFieldsFill()) return;
        Client c = new()
        {
            FirstName = FisrtNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Pin = PinTextBox.Text,
            Amount = Convert.ToDouble(AmountTextBox.Text, new NumberFormatInfo()),
            IsBlocked = bool.Parse((ReadOnlySpan<char>)IsBlockedTextBox.Text)
        };
        _model.DbClient.CreateUser(c);
        UpdateView();
       EmptyFields();
    }

    private void Button_Remove_Client_Click(object sender, RoutedEventArgs e)
    {
        if (IdTextBox.Text == String.Empty) return;
        _model.DbClient.DeleteClient(Convert.ToInt32(IdTextBox.Text));
        UpdateView();
        EmptyFields();
    }

    private void Button_Update_Client_Click(object sender, RoutedEventArgs e)
    {
        if (!IsEveryFieldsFill()) return;
        _model.DbClient.UpdateClient(new Client()
        {
            Id = Convert.ToInt32(IdTextBox.Text, new NumberFormatInfo()),
            FirstName = FisrtNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Pin = PinTextBox.Text,
            Amount = Convert.ToDouble(AmountTextBox.Text, new NumberFormatInfo()),
            IsBlocked = bool.Parse((ReadOnlySpan<char>)IsBlockedTextBox.Text)
        });
        UpdateView();
        EmptyFields();
    }

    private void Button_Load_Data_Click(object sender, RoutedEventArgs e)
    {
        UpdateView();
    }

    private void Search_Click(object sender, RoutedEventArgs e)
    {
        if(SearchBarTextBox.Text == String.Empty) return;
        DataGrid.ItemsSource = _model.DbClient.GetAll().Where(c => c.FirstName
            .Contains(SearchBarTextBox.Text, StringComparison.OrdinalIgnoreCase));
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        if(SearchBarTextBox.Text == String.Empty) return;
        UpdateView();
        SearchBarTextBox.Text = String.Empty;
    }

        private void UpdateView()
    {
        DataGrid.ItemsSource = _model.DbClient.GetAll();
        DataGrid.Items.Refresh();
    }
    private void Double_Click_On_Row(object sender, RoutedEventArgs e)
    {
        if (DataGrid.SelectedItem == null) return;
        Client c = DataGrid.SelectedItem as Client;
        IdTextBox.Text = c?.Id.ToString() ?? throw new InvalidOperationException();
        FisrtNameTextBox.Text = c.FirstName;
        LastNameTextBox.Text = c.LastName;
        PinTextBox.Text = c.Pin;
        IsBlockedTextBox.Text = c.IsBlocked.ToString();
        AmountTextBox.Text = c.Amount.ToString(CultureInfo.InvariantCulture);
    }

    private bool IsEveryFieldsFill()
    {
        return IdTextBox.Text != String.Empty && FisrtNameTextBox.Text != String.Empty
                                              && LastNameTextBox.Text != String.Empty
                                              && PinTextBox.Text != String.Empty
                                              && AmountTextBox.Text != String.Empty && IsBlockedTextBox.Text != String.Empty;
    }

    private void EmptyFields()
    {
        IdTextBox.Text = String.Empty;
        FisrtNameTextBox.Text = String.Empty;
        LastNameTextBox.Text = String.Empty;
        PinTextBox.Text = String.Empty;
        AmountTextBox.Text = String.Empty;
        IsBlockedTextBox.Text = String.Empty;
    }

   
    }
}
