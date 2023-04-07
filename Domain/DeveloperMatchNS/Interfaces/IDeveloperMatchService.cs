using Domain.DeveloperMatchNS.Queries;
using Domain.OrganizationNS;

namespace Domain.DeveloperMatchNS.Interfaces
{
    public interface IDeveloperMatchService
    {
        Task<bool> Add(DeveloperMatch match, CancellationToken cancellationToken);
        Task<IEnumerable<DeveloperMatch>> Get(DeveloperMatchQuery query);
        Task<bool> CheckForMatch(Guid developerUId, Guid organizationUId);
        Task<IEnumerable<Organization>> GetMyMatches(Guid developerUid);
        Task<bool> Delete(int id);
        Task<IEnumerable<Organization>> GetOrganizationsToMatch(Guid developerUid);
    }
}
