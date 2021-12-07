

using System;
using System.Globalization;
using System.Windows;
using BankingApp.Logic.Database;
using BankingApp.Logic.Model;

namespace BankingApp.Controller
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class LoginController
    {
        private ClientModel _model;
        public LoginController()
        {
            _model = new ClientModel(new ClientDBAccess());
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEveryFieldsFill())
            {
                MessageBox.Show("Please fill all fields before login");
                return;
            }

            Client c = _model.DbClient.GetClient(Convert.ToInt32(IdTextBox.Text, new NumberFormatInfo()));
            if (c == null)
            {
                EmptyFields();
                MessageBox.Show("Sorry user not found");
                return;
            }

            if (!Authentification(c))
            {
                MessageBox.Show("Sorry authentification failed. Login informations incorrect");
                return;
            }

            if (IsUserBlocked(c))
            {
                EmptyFields();
                MessageBox.Show("Sorry your bank account is blocked");
                return;
            }
            //Eviter la reconnexion du même client si celui a une session en cours
            //IsBlocked sera remis a false lorsque le client se déconnectera
            c.IsBlocked = true;
            _model.DbClient.UpdateClient(c);
            EmptyFields();
            ClientController clientViewController = new ClientController(c);
            clientViewController.Show();
        }

        private bool Authentification(Client c)
        {
            return IdTextBox.Text.Equals(c.Id.ToString()) && FirstNameTextBox.Text.Equals(c.FirstName)
                                                  && LastNameTextBox.Text.Equals(c.LastName) 
                                                  && PinPasswordBox.Password.Equals(c.Pin);
        }

        private bool IsEveryFieldsFill()
        {
            return IdTextBox.Text != String.Empty && FirstNameTextBox.Text != String.Empty
                                                  && LastNameTextBox.Text != String.Empty
                                                  && PinPasswordBox.Password != String.Empty;
        }

        private bool IsUserBlocked(Client c)
        {
            return c.IsBlocked;
        }

        private void EmptyFields()
        {
            IdTextBox.Text = String.Empty;
            FirstNameTextBox.Text = String.Empty;
            LastNameTextBox.Text = String.Empty;
            PinPasswordBox.Password = String.Empty;
        }
    }
}
