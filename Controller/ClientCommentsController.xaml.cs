using System;
using System.Windows;
using BankingApp.Logic.MOM;


namespace BankingApp.Controller
{
    /// <summary>
    /// Logique d'interaction pour ClientCommentsView.xaml
    /// </summary>
    public partial class ClientCommentsController
    {
        public ClientCommentsController()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Receive();
        }

        private void Receive()
        {
            string item = Consumer.ReceiveMessage();
            if (!item.Contains(Consumer.NO_MESSAGE))
            {
                NoMessageLabel.Content = String.Empty;
                ListOfComments.Items.Add(item);

            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ListOfComments.Items.Clear();
            NoMessageLabel.Content = "No message receive";
        }
    }
}
