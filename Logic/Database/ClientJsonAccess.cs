using System;
using System.Collections.Generic;
using BankingApp.Logic.DbJson;
using BankingApp.Logic.Model;

namespace BankingApp.Logic.Database
{
    public class ClientJsonAccess : IClientDataAccess
    {
        private readonly ClientFileManager dbClient = new();
        
        public List<Client> GetAll()
        {
            return dbClient.GetAllRecords();
        }

        public bool CreateUser(Client c)
        {
            try
            {
                dbClient.CreateRecord(c);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Echec création client Json database");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public Client GetClient(int Id)
        {
            return dbClient.GetRecord(Id);
        }

        public bool UpdateClient(Client c)
        {
            try
            {
                dbClient.UpdateRecord(c);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Echec mise à jour client Json database");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool DeleteClient(int Id)
        {
            try
            {
                dbClient.DeleteRecord(Id);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Echec suppression client Json database");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}