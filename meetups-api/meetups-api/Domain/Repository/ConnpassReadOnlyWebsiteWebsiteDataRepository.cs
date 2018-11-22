using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
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


        public ConnpassEventDataEntity convert(ConnpassEvent targetData)
        {
            var entity = new ConnpassEventDataEntity();
            entity.Id = targetData.event_id;
            entity.EventTitle = targetData.title ?? "";
            entity.EventUrl = targetData.event_url ?? "";
            entity.EventDescription = targetData.description ?? "";

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