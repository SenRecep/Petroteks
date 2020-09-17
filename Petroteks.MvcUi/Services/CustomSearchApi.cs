using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Petroteks.MvcUi.Models;

namespace GCSAPI
{
    public partial class CustomSearchApi
    {
        #region Fields
        private const int pageCount = 10;
        private string q = "sondaj";
        private string cx = "a4850c38bbf9e8f24";
        private string apiKey = "AIzaSyB0sT5UJ5fyTtEKrfYzQcisK_-TcvqmkO0";
        private string linkBase = "https://www.googleapis.com/customsearch/v1?";
        private string link = string.Empty;
        private int startIndex = 1;
        private List<GcsEntity> entities;
        #endregion

        #region Readonly Fields
        private readonly HttpClient client;
        #endregion

        #region Functions
        private void linkInit() => link = $"{linkBase}key={apiKey}&cx={cx}&q={q}&start={startIndex}";

        private async Task<dynamic> post()
        {
            var data = await client.GetStringAsync(link);
            return JsonConvert.DeserializeObject(data);
        }
        private void entityMapping(dynamic items)
        {
            if (items != null)
                foreach (var entity in items)
                    entities.Add(new GcsEntity(entity));
        }
        private void startIndexIncrement(dynamic nextPage) => this.startIndex = (int)nextPage?[0]?.startIndex;

        private async Task search()
        {
            linkInit();
            var data = await post();
            var nextPage = data.queries.nextPage;
            if (nextPage != null)
            {
                startIndexIncrement(nextPage);
                entityMapping(data.items);
                await search();
            }
            else if (data.items != null)
                entityMapping(data.items);
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

        public async Task<List<GcsEntity>> Search(string s)
        {
            this.q = s;
            entities = new List<GcsEntity>();
            startIndex = 1;
            await search();
            return entities;
        }

    }
}