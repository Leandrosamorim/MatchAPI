using Domain.DeveloperMatchNS.Interfaces;
using Domain.DeveloperMatchNS;
using Domain.OrganizationNS.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.DeveloperNS.Interfaces;
using Domain.OrganizationMatchNS.Interfaces;
using Domain.OrganizationMatchNS;

namespace MatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationMatchController : ControllerBase
    {
        private readonly IOrganizationMatchService _service;
        private readonly IDeveloperHttpService _http;

        public OrganizationMatchController(IOrganizationMatchService service, IDeveloperHttpService http)
        {
            _service = service;
            _http = http;
        }

        [HttpGet]
        [EnableCors("OrganizationAPI")]
        public async Task<IActionResult> GetDevelopersToMatch([FromQuery]Guid organizationUId, int stackId)
        {
            try
            {
                var developers = await _service.GetDevelopersToMatch(organizationUId, stackId);
                if(developers == null)
                {
                    return NotFound();
                }
                return Ok(developers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [EnableCors("OrganizationAPI")]
        public async Task<IActionResult> MatchOrganization(OrganizationMatch match)
        {
            try
            {
                await _service.Add(match, new CancellationToken());

                var matched = await _service.CheckForMatch(match.DeveloperUId, match.OrganizationUId);
                if (matched)
                    return Ok(true);
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("my")]
        [EnableCors("OrganizationAPI")]
        public async Task<IActionResult> GetMyMatches(Guid organizationUId)
        {
            var matches = await _service.GetMyMatches(organizationUId);
            if (matches == null)
                return NoContent();
            return Ok(matches);
        }
    }
}
