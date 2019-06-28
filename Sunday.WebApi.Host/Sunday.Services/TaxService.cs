using AutoMapper;
using Sunday.Repositories;
using Sunday.WebApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunday.Services
{
    public class TaxService
    {
        public IRepository<Repositories.Entities.Tax> TaxRepository { get; set; }
        public IMapper Mapper { get; set; }

        public async Task<Guid> Add(Tax tax)
        {
            var dto = Mapper.Map<Repositories.Entities.Tax>(tax);

            return await TaxRepository.Add(dto);
        }
        
        public async Task Update(Tax tax)
        {
            var dto = Mapper.Map<Repositories.Entities.Tax>(tax);
            await TaxRepository.Update(dto);
        }

        public async Task<Tax> Get(Guid uuid)
        {
            return Mapper.Map<Tax>(await TaxRepository.Get(uuid));
        }
        
        public async Task<List<Tax>> List()
        {
            return (await TaxRepository.List()).Select(Mapper.Map<Tax>).ToList();
        }
    }
}
