using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Petroteks.MvcUi.Models;

namespace GCSAPI
{
    public class CustomSearchSetting{
        public string Cx { get; set; }
        public string ApiKey { get; set; }
        public string LinkBase { get; set; }
    }
    public partial class CustomSearchApi
    {
        #region Fields
        private CustomSearchSetting setting;
        private string q = string.Empty;
        private string link = string.Empty;
        private int startIndex = 1;
        private List<GcsEntity> entities;
        #endregion

        #region Readonly Fields
        private readonly HttpClient client;
        #endregion

        #region Functions
        private void linkInit() => link = $"{setting.LinkBase}key={setting.ApiKey}&cx={setting.Cx}&q={q}&start={startIndex}";

        private async Task<dynamic> post()
        {
            try
            {
                var data = await client.GetStringAsync(link);
                return JsonConvert.DeserializeObject(data);
            }
            catch 
            {
                return null;
            }
         
        }
        private void entityMapping(dynamic items)
        {
            if (items != null)
                foreach (var entity in items)
                    entities.Add(new GcsEntity(entity));
        }
        private void startIndexIncrement(dynamic nextPage) => this.startIndex = (int)nextPage?[0]?.startIndex;

        private async Task<bool> search()
        {
            linkInit();
            var data = await post();
            if (data!=null)
            {
                var nextPage = data.queries.nextPage;
                if (nextPage != null)
                {
                    startIndexIncrement(nextPage);
                    entityMapping(data.items);
                    await search();
                }
                else if (data.items != null)
                    entityMapping(data.items);
                return true;
            }
            return false;
        }

        #endregion

    }
    public partial class CustomSearchApi
    {
        #region  Contractors
        public CustomSearchApi(HttpClient _client,IOptions<CustomSearchSetting> option)
        {
            client = _client;
            this.Init(option.Value);
        }
        #endregion

        public void Init(CustomSearchSetting setting){
          this.setting=setting;
        }
        public async Task<List<GcsEntity>> Search(string s)
        {
            this.q = s;
            entities = new List<GcsEntity>();
            startIndex = 1;
            if (await search()==false)
                return null;
            return entities;
        }

    }
}