using System;
using System.Collections.Generic;
using System.IO;
using BankingApp.Logic.Model;

namespace BankingApp.Logic.DbJson
{
    class ClientFileManager
    {
        public void CreateRecord(Client c)
        {   //Pour éviter les redondances
            if (!IsAlreadyCreated(c.Id))
            {
                StreamWriter sw = File.AppendText(FilePath.CLIENT_TABLE_PATH);
                sw.WriteLine(JsonHelper<Client>.Serialize(c));//Ajout d'un enregistrement dans la table Client
                sw.Close();
                UpdateBankDb(c.Id, "Add");
            }
        }

        public Client GetRecord(int Id)
        {
            if (IsAlreadyCreated(Id))
            {
                string row = GetRow(new StreamReader(FilePath.CLIENT_TABLE_PATH), Id);
                if(row != String.Empty)
                   return JsonHelper<Client>.DeSerialize<Client>(row);
            }
            return null;
        }

        public List<Client> GetAllRecords()
        {
            List<Client> clients = new List<Client>();
            string row;
            StreamReader rd = new StreamReader(FilePath.CLIENT_TABLE_PATH);
            while((row = rd.ReadLine()) != null)
                clients.Add(JsonHelper<Client>.DeSerialize<Client>(row));
            rd.Close();
            return clients;
        }

        public void UpdateRecord(Client c)
        {
            if (IsAlreadyCreated(c.Id))
            {
                string tempFile = Path.GetTempFileName();
                StreamReader rd = new StreamReader(FilePath.CLIENT_TABLE_PATH);
                StreamWriter wr = new StreamWriter(tempFile);
                string row = String.Empty;
                while ((row = rd.ReadLine()) != null)
                {
                    if (row.Contains("\"Id\":" + c.Id))
                    {
                        wr.WriteLine(JsonHelper<Client>.Serialize(c));//Ajout de la ligne mise à jour
                        continue;
                    }
                    wr.WriteLine(row);
                }
                rd.Close();
                wr.Close();
                File.Delete(FilePath.CLIENT_TABLE_PATH);
                File.Move(tempFile, FilePath.CLIENT_TABLE_PATH);
            }

        }

        public void DeleteRecord(int Id)
        {
            if (IsAlreadyCreated(Id))
            {
                string tempFile = Path.GetTempFileName();
                StreamReader rd = new StreamReader(FilePath.CLIENT_TABLE_PATH);
                StreamWriter wr = new StreamWriter(tempFile);
                string row = String.Empty;
                while ((row = rd.ReadLine()) != null)
                {
                    if (row.Contains("\"Id\":" + Id))
                        continue;//On ne réecrit pas la ligne à supprimer
                    wr.WriteLine(row);
                }
                rd.Close();
                wr.Close();
                File.Delete(FilePath.CLIENT_TABLE_PATH);
                File.Move(tempFile, FilePath.CLIENT_TABLE_PATH);
                UpdateBankDb(Id, "Rm");
            }
        }

        protected void UpdateBankDb(int clientId, string option)
        {
            string tempFile = Path.GetTempFileName();
            StreamReader rd = new StreamReader(FilePath.BANK_DB_PATH);
            StreamWriter wr = new StreamWriter(tempFile);
            string row = String.Empty;
            int counter = 0;
            while ((row = rd.ReadLine()) != null)
            {
                if (option.Equals("Rm", StringComparison.OrdinalIgnoreCase)){}
                    if (row.Contains("Client_" + clientId))
                        continue;
                if(row.Contains("NUMBER OF CLIENT"))
                    continue;
                wr.WriteLine(row);
                counter++;
            }
            if (option.Equals("Add", StringComparison.OrdinalIgnoreCase))
            {
                //Ajout d'un nouveau client dans la database
                wr.WriteLine("Client_" + clientId);
                counter++;
            }
            wr.WriteLine("NUMBER OF CLIENT : " + counter);
            rd.Close();
            wr.Close();
            File.Delete(FilePath.BANK_DB_PATH);
            File.Move(tempFile, FilePath.BANK_DB_PATH);
        }

        protected string GetRow(StreamReader rd, int Id)
        {
            string row = null;
            bool cdt = false;
            while (cdt == false && (row = rd.ReadLine()) != null)
                if (row.Contains("\"Id\":" + Id))
                    cdt = true;

            rd.Close();
            return row;
        }

        protected bool IsAlreadyCreated(int Id)
        {
            var s = File.ReadAllText(FilePath.BANK_DB_PATH);
            return s.Contains("Client_" + Id) ? true : false;
        }
    }

   public class FilePath
    {
        public static readonly string BANK_DB_PATH = Environment.CurrentDirectory + "/DbJson/BankDatabaseJson.txt";//Emplacement du fichier BankDatabaseJson.txt
        public static readonly string CLIENT_TABLE_PATH = Environment.CurrentDirectory + "/DbJson/ClientTable.txt";//Emplacement du fichier ClientTable.txt
    }
}
