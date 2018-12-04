using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Domain.Usecase;
using meetupsApi.JsonEntity;
using meetupsApi.Tests.Domain.Usecase;

namespace meetupsApi.Tests.Domain.Repository
{
    public class ConnpassReadOnlyWebsiteWebsiteDataRepository : IConnpassReadOnlyWebsiteDataRepository
    {
        private readonly IConnpassDataStore _connpassDatastore;

        public ConnpassReadOnlyWebsiteWebsiteDataRepository(IConnpassDataStore connpassDatastore)
        {
            _connpassDatastore = connpassDatastore;
        }

        public async Task<IEnumerable<ConnpassEventDataEntity>> LoadConnpassData(int page = 0)
        {
            var jsonData = await _connpassDatastore.LoadConnpassDataAsync(100, page);
            return jsonData.ConnpassEvents.Select(item => convert(item));
        }

        public Task<IEnumerable<ConnpassEventDataEntity>> LoadSpecificConnpassDataAsync(int start = 1)
        {
            throw new System.NotImplementedException();
        }


        public ConnpassEventDataEntity convert(ConnpassEvent targetData)
        {
            var entity = new ConnpassEventDataEntity();
            entity.Id = targetData.event_id;
            entity.title = targetData.title ?? "";
            entity.event_url = targetData.event_url ?? "";
            entity.description = targetData.description ?? "";
            entity.catchMesagge = targetData._catch;
            entity.hash_tag = targetData.hash_tag;
            entity.started_at = targetData.started_at;
            entity.ended_at = targetData.ended_at;


            int ToInt(
                string value,
                int defaultValue = 0)
                => int.TryParse(value, out var i) ? int.Parse(value) : defaultValue;

            entity.limit = targetData.limit ?? 0;
            entity.accepted = targetData.accepted;
            entity.waiting = targetData.waiting;
            entity.updated_at = targetData.updated_at;

            entity.event_type = targetData.event_type;
            entity.address = targetData.address;
            entity.owned_id = targetData.owner_id;
            entity.owned_nickname = targetData.owner_nickname;
            entity.owner_display_name = targetData.owner_display_name;

            double ToDouble(
                string value,
                double defaultValue = double.MaxValue
            ) => double.TryParse(value, out var i) ? double.Parse(value) : defaultValue;

            entity.Lon = ToDouble(targetData.lon);
            entity.Lat = ToDouble(targetData.lat);
            return entity;
        }
    }
}