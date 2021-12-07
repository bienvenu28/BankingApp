using System;
using System.Globalization;
using System.Text.Json.Serialization;
using BankingApp.Logic.DbJson;
using RestSharp;

namespace BankingApp.Logic.CurrencyFreaksApi
{
    public class Currency
    {
        public static readonly string API_KEY = "0003cbc021aa468cb6691794fe09ff94";
        public static readonly string BITCOIN_CASH = "BCH";
        public static readonly string ANGOLA_KWANZA = "AOA";
        public static readonly string COLOMBIAN_PESO = "COP";
        public static readonly string CANADIAN_DOLLAR = "CAD";
        public static readonly string EURO = "EUR";
        public static readonly string UAE_DIRHAM = "AED";
        public static readonly string CHINESE_YUAN_RENMINBI = "CNH";
        public static readonly string ETHEREUM = "ETH";
        public static readonly string RUSSIAN_RUBLE = "RUB";

        public Rootobject CurrencyList { get; set; }

        public void LoadCurrencies()
        {
            var client = new RestClient("https://api.currencyfreaks.com/latest?apikey=" + API_KEY + "&symbols=" + GetAllSymbols());
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            CurrencyList = JsonHelper<Rootobject>.DeSerialize<Rootobject>(response.Content);
        }

        public double GetAmountIn(double amount, string targetCurrency)
        {
            if (targetCurrency == BITCOIN_CASH)
                return amount * Convert.ToDouble(CurrencyList.Rates.BCH, new NumberFormatInfo());
            else if (targetCurrency == ANGOLA_KWANZA)
                return amount * Convert.ToDouble(CurrencyList.Rates.AOA, new NumberFormatInfo());
            else if (targetCurrency == COLOMBIAN_PESO)
                return amount * Convert.ToDouble(CurrencyList.Rates.COP, new NumberFormatInfo());
            else if (targetCurrency == CANADIAN_DOLLAR)
                return amount * Convert.ToDouble(CurrencyList.Rates.CAD, new NumberFormatInfo());
            else if (targetCurrency == EURO)
                return amount * Convert.ToDouble(CurrencyList.Rates.EUR, new NumberFormatInfo());
            else if (targetCurrency == UAE_DIRHAM)
                return amount * Convert.ToDouble(CurrencyList.Rates.AED, new NumberFormatInfo());
            else if (targetCurrency == CHINESE_YUAN_RENMINBI)
                return amount * Convert.ToDouble(CurrencyList.Rates.CNH, new NumberFormatInfo());
            else if (targetCurrency == ETHEREUM)
                return amount * Convert.ToDouble(CurrencyList.Rates.ETH, new NumberFormatInfo());
            else
                return amount * Convert.ToDouble(CurrencyList.Rates.RUB, new NumberFormatInfo());
        }

        protected string GetAllSymbols()
        {
            return BITCOIN_CASH + "," + ANGOLA_KWANZA + "," + COLOMBIAN_PESO + ","
                   + CANADIAN_DOLLAR + "," + EURO + "," + UAE_DIRHAM + ","
                   + CHINESE_YUAN_RENMINBI + "," + ETHEREUM + "," + RUSSIAN_RUBLE;
        }

    }

    public class Rootobject
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("rates")]
        public Rates Rates { get; set; }
    }

    public class Rates
    {
        [JsonPropertyName("BCH")]
        public string BCH { get; set; }
        [JsonPropertyName("AOA")]
        public string AOA { get; set; }
        [JsonPropertyName("COP")]
        public string COP { get; set; }
        [JsonPropertyName("CAD")]
        public string CAD { get; set; }
        [JsonPropertyName("EUR")]
        public string EUR { get; set; }
        [JsonPropertyName("AED")]
        public string AED { get; set; }
        [JsonPropertyName("CNH")]
        public string CNH { get; set; }
        [JsonPropertyName("ETH")]
        public string ETH { get; set; }
        [JsonPropertyName("RUB")]
        public string RUB { get; set; }
    }
   
}