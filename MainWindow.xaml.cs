using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankingApp.Controller;

namespace BankingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientTableController clientController;
        private LoginController loginController;
        private ClientCommentsController clientCommentsController;
        private JsonDbController jsonDbController;
        private HomeController homeController;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Sign_In_Click(object sender, RoutedEventArgs e)
        {
            loginController ??= new LoginController();
            MainContentFrame.NavigationService.Navigate(loginController);
        }
        private void Button_Clients_Click(object sender, RoutedEventArgs e)
        {
            clientController ??= new ClientTableController();
            MainContentFrame.NavigationService.Navigate(clientController);
        }
       
        private void Button_Json_Database_Click(object sender, RoutedEventArgs e)
        {
            jsonDbController ??= new JsonDbController();
            MainContentFrame.NavigationService.Navigate(jsonDbController);
        }

        private void Button_Client_Commments_Click(object sender, RoutedEventArgs e)
        {
            clientCommentsController ??= new ClientCommentsController();
            MainContentFrame.NavigationService.Navigate(clientCommentsController);
        }

        private void Button_Home_Click(object sender, RoutedEventArgs e)
        {
            homeController ??= new HomeController();
            MainContentFrame.NavigationService.Navigate(homeController);
        }
    }
}
