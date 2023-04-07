using Domain.DeveloperMatchNS.Queries;
using Domain.DeveloperMatchNS;
using System;
using Domain.OrganizationMatchNS.Queries;

namespace Domain.OrganizationMatchNS.Interfaces
{
    public interface IOrganizationMatchRepository
    {
        Task<IEnumerable<OrganizationMatch>> Get(OrganizationMatchQuery query, CancellationToken cancellationToken);
        Task<bool> Create(OrganizationMatch match, CancellationToken cancellationToken);
        Task<bool> Delete(int id);
        Task<bool> CheckForMatch(Guid developerUid, Guid organizationUid);
        Task<IEnumerable<Guid>> GetMyApprovals(Guid organizationUId);
        Task<IEnumerable<Guid>> GetMyMatches(Guid organizationUId);
    }
}
