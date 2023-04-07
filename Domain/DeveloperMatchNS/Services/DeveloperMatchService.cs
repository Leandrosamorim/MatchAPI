using Domain.DeveloperMatchNS.Interfaces;
using Domain.DeveloperMatchNS.Queries;
using Domain.OrganizationNS;
using Domain.OrganizationNS.Interfaces;

namespace Domain.DeveloperMatchNS.Services
{
    public class DeveloperMatchService : IDeveloperMatchService
    {
        private readonly IDeveloperMatchRepository _repository;
        private readonly IOrganizationHttpService _http;

        public DeveloperMatchService(IDeveloperMatchRepository repository, IOrganizationHttpService http)
        {
            _repository = repository;
            _http = http;
        }

        public async Task<bool> Add(DeveloperMatch match, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Create(match, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<bool> CheckForMatch(Guid developerUId, Guid OrganizationUId)
        {
            return await _repository.CheckForMatch(developerUId, OrganizationUId);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<DeveloperMatch>> Get(DeveloperMatchQuery query)
        {
            return await _repository.Get(query, new CancellationToken());
        }

        public async Task<IEnumerable<Organization>> GetMyMatches(Guid developerUid)
        {
            var organizationGuids = await _repository.GetMyMatches(developerUid);
            var organizations = await _http.GetOrganizationsByUId(organizationGuids);
            return organizations;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsToMatch(Guid developerUid)
        {
            var organizationGuids = await _repository.GetMyMatches(developerUid);
            var organizations = await _http.GetAll();
            var organizationsToMatch = organizations.Where(x => !organizationGuids.Contains(x.UId)).ToList();
            return organizationsToMatch;
        }
    }
}
