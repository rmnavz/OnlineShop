using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Online_Shop.Code.RestSharp.CurrencyConverterApi
{
    public class FreeCurrencyConverterApi
    {
        const string BaseUrl = "https://free.currencyconverterapi.com";

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var CurrencyConverterException = new ApplicationException(message, response.ErrorException);
                throw CurrencyConverterException;
            }
            return response.Data;
        }
        
        public string ExecuteJSON(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var CurrencyConverterException = new ApplicationException(message, response.ErrorException);
                throw CurrencyConverterException;
            }
            return response.Content;
        }

        public ConvertResult GetExchangeRate(CultureInfo Currency) 
        {
            var OutputCultureInfo = CultureInfo.CurrentCulture;
            var Output = new RegionInfo(OutputCultureInfo.LCID);
            var current = new RegionInfo(Currency.LCID);
            var Query = current.ISOCurrencySymbol + '_' + Output.ISOCurrencySymbol;

            var request = new RestRequest();
            request.Resource = "api/v6/convert?q={Query}";
            request.AddUrlSegment("Query", Query);

            JObject jObject = JObject.Parse(ExecuteJSON(request));
            jObject = jObject.SelectToken("results", false).ToObject<JObject>();
            return jObject.SelectToken(Query, false).ToObject<ConvertResult>();
        }
    }
}