using System.Net.Http;
using System.Threading.Tasks;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Repository;
using Newtonsoft.Json;

namespace meetupsApi.Tests.Repository
{
    public class ConnpassDatastore : IConnpassDataStore
    {
        private async Task<string> loadJsonAwait(int capacity = 100)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("C#/ASP.net Core");
                var response = await client.GetAsync($"https://connpass.com/api/v1/event/?count={capacity}");
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100)
        {
            var json = await loadJsonAwait(capacity);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ConnpassMeetupJson>(json);
        }
    }
}