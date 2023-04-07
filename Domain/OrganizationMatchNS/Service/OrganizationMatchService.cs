using Domain.DeveloperNS;
using Domain.DeveloperNS.Interfaces;
using Domain.OrganizationMatchNS.Interfaces;
using Domain.OrganizationMatchNS.Queries;
using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Domain.OrganizationMatchNS.Service
{
    public class OrganizationMatchService : IOrganizationMatchService
    {
        private readonly IOrganizationMatchRepository _organizationMatchRepository;
        private readonly IDeveloperHttpService _http;

        public OrganizationMatchService(IOrganizationMatchRepository organizationMatchRepository, IDeveloperHttpService http)
        {
            _organizationMatchRepository = organizationMatchRepository;
            _http = http;
        }

        public async Task<bool> Add(OrganizationMatch match, CancellationToken cancellationToken)
        {
            try
            {
                await _organizationMatchRepository.Create(match, new CancellationToken());
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
          
        }

        public async Task<bool> CheckForMatch(Guid developerUId, Guid OrganizationUId)
        {
            return await _organizationMatchRepository.CheckForMatch(developerUId, OrganizationUId);
        }

        public async Task<bool> Delete(int id)
        {
            return await _organizationMatchRepository.Delete(id);
        }

        public async Task<IEnumerable<OrganizationMatch>> Get(OrganizationMatchQuery query)
        {
            return await _organizationMatchRepository.Get(query, new CancellationToken());
        }

        public async Task<IEnumerable<Developer>> GetDevelopersToMatch(Guid organizationUid, int stackId)
        {
            var matchedDevelopers = await _organizationMatchRepository.GetMyMatches(organizationUid);
            var developers = await _http.GetByStack(stackId);
            var developersToMatch = developers.Where(x => !matchedDevelopers.Contains(x.UId)).ToList();
            return developersToMatch;
        }

        public async Task<IEnumerable<Developer>> GetMyMatches(Guid organizationUid)
        {
            var developerGuids = await _organizationMatchRepository.GetMyMatches(organizationUid);
            var developers = await _http.GetDevelopersByUId(developerGuids);
            return developers;
        }
    }
}
