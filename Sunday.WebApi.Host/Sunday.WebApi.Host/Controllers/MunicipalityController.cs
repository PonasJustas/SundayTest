using Newtonsoft.Json;
using Sunday.Services;
using Sunday.WebApi.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sunday.WebApi.Host.Controllers
{
    public class MunicipalityController : ApiController
    {
        public MunicipalityService MunicipalityService { get; set; }

        [HttpGet]
        [Route("api/municipality/tax/{name}/{date}")]
        [ResponseType(typeof(Municipality))]
        public async Task<IHttpActionResult> Get(string name, DateTime date)
        {
            var tax = await MunicipalityService.GetTax(name, date);
            if (tax == null)
            {
                return NotFound();
            }

            return Ok(tax.Value);
        }

        [HttpGet]
        [Route("api/municipality/")]
        [ResponseType(typeof(IEnumerable<Municipality>))]
        public async Task<IHttpActionResult> Get()
        {
            var municipalities = await MunicipalityService.List();

            return Ok(municipalities);
        }

        [HttpPut]
        [Route("api/municipality/{uuid}/{name}")]
        public async Task<IHttpActionResult> Update(Guid uuid, string name)
        {
            try
            {
                var municipality = new Municipality { Uuid = uuid, Name = name };
                await MunicipalityService.Update(municipality);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("api/municipality/{name}")]
        [ResponseType(typeof(Guid))]
        public async Task<IHttpActionResult> Add(string name, [FromBody] List<Tax> taxes)
        {
            try
            {
                var municipality = new Municipality { Name = name, Taxes = taxes };
                var entityUuid = await MunicipalityService.Add(municipality);

                if (entityUuid == Guid.Empty)
                {
                    return BadRequest();
                }

                return Ok(entityUuid);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/municipality/upload")]
        public async Task<IHttpActionResult> ImportMunicipalities()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var municipalities = JsonConvert.DeserializeObject<List<Municipality>>(await file.ReadAsStringAsync());

                await MunicipalityService.AddBulk(municipalities);
            }

            return Ok();
        }
    }
}
