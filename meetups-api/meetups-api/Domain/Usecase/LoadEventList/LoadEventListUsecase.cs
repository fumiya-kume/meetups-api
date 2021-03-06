using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupsApi.Domain.Entity;
using meetupsApi.Tests.Domain.Usecase;

namespace meetupsApi.Domain.Usecase.LoadEventList
{
    public class LoadEventListUsecase : ILoadEventListUsecase
     {
         private readonly IConnpassDatabaseRepository _connpassDatabaseRepository;
 
         public LoadEventListUsecase(IConnpassDatabaseRepository connpassDatabaseRepository)
         {
             _connpassDatabaseRepository = connpassDatabaseRepository;
         }
 
         public async Task<List<ConnpassEventDataEntity>> Execute(int count = 1500)
         {
             return (await _connpassDatabaseRepository.loadEventList(count)).ToList();
         }
     }
 }