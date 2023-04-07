using Domain.DeveloperMatchNS;
using Domain.DeveloperMatchNS.Interfaces;
using Domain.OrganizationNS.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperMatchController : ControllerBase
    {
        private readonly IDeveloperMatchService _service;
        private readonly IOrganizationHttpService _http;

        public DeveloperMatchController(IDeveloperMatchService service, IOrganizationHttpService http)
        {
            _service = service;
            _http = http;
        }

        [HttpGet]
        [EnableCors("DeveloperAPI")]
        public async Task<IActionResult> GetOrganizationsToMatch(Guid developerUId)
        {
            try
            {
                var organizations = await _service.GetOrganizationsToMatch(developerUId);
                return Ok(organizations);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [EnableCors("DeveloperAPI")]
        public async Task<IActionResult> MatchOrganization(DeveloperMatch match)
        {
            try
            {
                await _service.Add(match, new CancellationToken());

                var matched = await _service.CheckForMatch(match.DeveloperUId, match.OrganizationUId);
                if (matched)
                    return Ok(true);
                else
                    return null;

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("my")]
        [EnableCors("DeveloperAPI")]
        public async Task<IActionResult> GetMyMatches(Guid developerUId)
        {
            var matches = await _service.GetMyMatches(developerUId);
            if(matches == null)
                return NoContent();
            return Ok(matches);
        }

    }
}
