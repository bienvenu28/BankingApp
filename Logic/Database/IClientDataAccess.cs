using System.Collections.Generic;
using BankingApp.Logic.Model;

namespace BankingApp.Logic.Database
{
    public interface IClientDataAccess
    {
        List<Client> GetAll();
        bool CreateUser(Client c);
        Client GetClient(int Id);
        bool UpdateClient(Client c);
        bool DeleteClient(int Id);
    }
}
