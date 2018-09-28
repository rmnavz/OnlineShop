namespace OnlineShop.Code.RestSharp.CurrencyConverterApi
{
    public class ConvertResponse
    {

    }
    public class ConvertResult
    {

        public string id { get; set; }
        public string fr { get; set; }
        public string to { get; set; }
        public decimal val { get; set; }
    }
}