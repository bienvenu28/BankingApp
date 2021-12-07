using System.Text.Json;


namespace BankingApp.Logic.DbJson
{
    public class JsonHelper<T>
    {
       public static string Serialize(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T DeSerialize<T>(string jsonObj)
        {
            return JsonSerializer.Deserialize<T>(jsonObj);
        }
    }
}