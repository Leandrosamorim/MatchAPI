using Domain.DeveloperMatchNS.Queries;
using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperMatchNS.Interfaces
{
    public interface IDeveloperMatchRepository
    {
        Task<IEnumerable<DeveloperMatch>> Get(DeveloperMatchQuery query, CancellationToken cancellationToken);
        Task<bool> Create(DeveloperMatch match, CancellationToken cancellationToken);
        Task<bool> Delete(int Id);
        Task<bool> CheckForMatch(Guid developerUid, Guid organizationUid);
        Task<IEnumerable<Guid>> GetMyApprovals(Guid developerUid);
        Task<IEnumerable<Guid>> GetMyMatches(Guid developerUid);
    }
}
