using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using BankingApp.Logic.Database;
using BankingApp.Logic.DbJson;
using BankingApp.Logic.Model;

namespace BankingApp.Controller
{
    public partial  class JsonDbController
    {
        private ClientModel _modelSQLiteDb;
        private ClientModel _modelJsonDb;

        public JsonDbController()
        {
            _modelSQLiteDb = new ClientModel(new ClientDBAccess());
            _modelJsonDb = new ClientModel(new ClientJsonAccess());
            InitializeComponent();
        }

        private void SyncDb_Click(object sender, RoutedEventArgs e)
        {
            List<Client> result = _modelSQLiteDb.DbClient.GetAll();
            foreach (var c in result)
            {
                if (_modelJsonDb.DbClient.GetClient(c.Id) == null)
                    _modelJsonDb.DbClient.CreateUser(c);
                _modelJsonDb.DbClient.UpdateClient(c);
            }
            NoDataLabel.Content = String.Empty;
            JsonDbContentListView.ItemsSource = _modelJsonDb.DbClient.GetAll();
            BankTextBlock.Text = File.ReadAllText(FilePath.BANK_DB_PATH);
        }

        private void ClearContent_Click(object sender, RoutedEventArgs e)
        {
            JsonDbContentListView.ItemsSource = null;
            NoDataLabel.Content = "No Data display";
            BankTextBlock.Text = String.Empty;
        }
    }
}
