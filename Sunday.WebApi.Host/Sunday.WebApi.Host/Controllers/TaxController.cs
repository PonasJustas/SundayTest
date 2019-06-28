using Sunday.Services;
using Sunday.WebApi.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sunday.WebApi.Host.Controllers
{
    public class TaxController : ApiController
    {
        public TaxService TaxService { get; set; }

        [HttpGet]
        [Route("api/tax/")]
        [ResponseType(typeof(IEnumerable<Tax>))]
        public async Task<IHttpActionResult> Get()
        {
            var municipalities = await TaxService.List();

            return Ok(municipalities);
        }

        [HttpPut]
        [Route("api/tax/{uuid}/{municipalityUuid}/{startPeriod}/{schedule}")]
        public async Task<IHttpActionResult> Update(Guid uuid, Guid municipalityUuid, DateTime startPeriod, TaxSchedule schedule)
        {
            try
            {
                var tax = new Tax { Uuid = uuid, MunicipalityUuid = municipalityUuid, StartPeriod = startPeriod, Schedule = schedule};
                await TaxService.Update(tax);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("api/tax/{municipalityUuid}/{startPeriod}/{schedule}/{value}")]
        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> Add(Guid municipalityUuid, DateTime startPeriod, TaxSchedule schedule, double value)
        {
            var tax = new Tax
                { MunicipalityUuid = municipalityUuid, StartPeriod = startPeriod, Schedule = schedule, Value = value };
            var entityUuid = await TaxService.Add(tax);

            if (entityUuid == Guid.Empty)
            {
                return BadRequest();
            }

            return Ok(entityUuid);
        }
    }
}
