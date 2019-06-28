using AutoMapper;
using Sunday.Repositories;
using Sunday.WebApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunday.Services
{
    public class MunicipalityService
    {
        public IRepository<Repositories.Entities.Municipality> MunicipalityRepository { get; set; }
        public IMapper Mapper { get; set; }

        public async Task<Guid> Add(Municipality municipality)
        {
            var dto = Mapper.Map<Repositories.Entities.Municipality>(municipality);

            return await MunicipalityRepository.Add(dto);
        }

        public async Task AddBulk(IEnumerable<Municipality> municipalities)
        {
            var dto = municipalities.Select(Mapper.Map<Repositories.Entities.Municipality>);

            await MunicipalityRepository.AddBulk(dto);
        }

        public async Task Update(Municipality municipality)
        {
            var dto = Mapper.Map<Repositories.Entities.Municipality>(municipality);
            await MunicipalityRepository.Update(dto);
        }

        public async Task<Municipality> Get(Guid uuid)
        {
            return Mapper.Map<Municipality>(await MunicipalityRepository.Get(uuid));
        }

        public async Task<Municipality> Get(string name)
        {
            return Mapper.Map<Municipality>(await MunicipalityRepository.Get(name));
        }

        public async Task<List<Municipality>> List()
        {
            return (await MunicipalityRepository.List()).Select(Mapper.Map<Municipality>).ToList();
        }

        public async Task<Tax> GetTax(string name, DateTime date)
        {
            var municipality = await Get(name);

            return municipality?.Taxes.OrderBy(x => x.Schedule).FirstOrDefault(tax => tax.StartPeriod <= date.Date && CalculatePeriodEnd(tax) >= date.Date);
        }

        private DateTime CalculatePeriodEnd(Tax tax)
        {
            switch (tax.Schedule)
            {
                case TaxSchedule.Yearly:
                    return tax.StartPeriod.AddYears(1).AddDays(-1);
                case TaxSchedule.Monthly:
                    return tax.StartPeriod.AddMonths(1).AddDays(-1);
                case TaxSchedule.Weekly:
                    return tax.StartPeriod.AddDays(6);
                case TaxSchedule.Daily:
                default:
                    return tax.StartPeriod;
            }
        }
    }
}
