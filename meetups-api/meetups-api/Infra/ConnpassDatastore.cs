using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Repository;
using Newtonsoft.Json;

namespace meetupsApi.Tests.Repository
{
    public class ConnpassDatastore : IConnpassDataStore
    {
        private async Task<string> loadJsonAwait(int capacity = 100, int page = 0)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("C#/ASP.net Core");
                var startCount = page == 0 ? 1 : page * 100;
                var response =
                    await client.GetAsync($"https://connpass.com/api/v1/event/?count={capacity}&start={startCount}");
                if (response.StatusCode == HttpStatusCode.BadGateway)
                {
                    return "502";
                }

                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<ConnpassMeetupJson> LoadConnpassDataAsync(int capacity = 100, int page = 0)
        {
            var json = await loadJsonAwait(capacity, page);
            if (json == "502")
            {
                return new ConnpassMeetupJson();
            }

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ConnpassMeetupJson>(json);
        }

        public Task<ConnpassMeetupJson> LoadSpecificConnpassDataAsync(int start = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}