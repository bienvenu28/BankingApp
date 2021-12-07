using BankingApp.Logic.Database;

namespace BankingApp.Logic.Model
{
    public class ClientModel
    {
        public ClientModel(IClientDataAccess db)
        {
            DbClient = db;
        }
        public IClientDataAccess DbClient { get; set; }
    }
}