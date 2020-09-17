using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace GCSAPI
{
    public partial class CustomSearchApi
    {
        #region Fields
        private string q = "sondaj";
        private string cx = "a4850c38bbf9e8f24";
        private string apiKey = "AIzaSyB0sT5UJ5fyTtEKrfYzQcisK_-TcvqmkO0";
        private string linkBase = "https://www.googleapis.com/customsearch/v1?";
        private string link = string.Empty;
        private int page = 1;
        #endregion

        #region Readonly Fields
        private readonly HttpClient client;
        #endregion

        #region Functions
        private void linkInit() => link = $"{linkBase}key={apiKey}%cx={cx}&q={q}&start={page}";

        private async Task<dynamic> post()
        {
            var data= await client.GetStringAsync(link);
            return JsonConvert.DeserializeObject(data);
        }
        #endregion

    }
    public partial class CustomSearchApi
    {
        #region  Contractors
        public CustomSearchApi(HttpClient _client)
        {
            client = _client;
        }
        #endregion

        public async Task Search(string s)
        {
            this.q = s;
            dynamic data = await post();
            Console.WriteLine(data.items.Count);
        }
    }
}