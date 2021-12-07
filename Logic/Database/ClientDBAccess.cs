using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using BankingApp.Logic.Model;

namespace BankingApp.Logic.Database
{
    class ClientDBAccess : IClientDataAccess
    {
        private readonly ClienContext dbClient = new();

        public List<Client> GetAll()
        {
            //Assurer que la base données existe, sinon on la crée
            dbClient.Database.EnsureCreated();
            var clients = dbClient.ClientList;
            if (clients == null)
                return null;
            return (from c in clients select c).ToList();
        }

        public bool CreateUser(Client c)
        {
            //Assurer que la base données existe, sinon on la crée
            dbClient.Database.EnsureCreated();
            try
            {
                dbClient.ClientList.Add(c);
                dbClient.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Creation of " + c.FirstName +" fails");
                return false;
            }
        }

        public Client GetClient(int Id)
        {
            //Assurer que la base données existe, sinon on la crée
            dbClient.Database.EnsureCreated();
            return dbClient.ClientList.Find(Id);
        }

        public bool UpdateClient(Client c)
        {
            //Assurer que la base données existe, sinon on la crée
            dbClient.Database.EnsureCreated();
            try
            {
                var trackedClient = dbClient.ClientList.Find(c.Id);
                dbClient.Entry(trackedClient).CurrentValues.SetValues(c);
                dbClient.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Update of " + c.FirstName + " fails");
                return false;
            }
        }

        public bool DeleteClient(int Id)
        {
            //Assurer que la base données existe, sinon on la crée
            dbClient.Database.EnsureCreated();
            try
            {
                var clients = dbClient.ClientList;
                Client clt = (from c in clients where c.Id == Id select c).Single();
                dbClient.ClientList.Remove(clt);
                dbClient.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Delete of " + Id + " fails");
                return false;
            }
        }
    }
}
