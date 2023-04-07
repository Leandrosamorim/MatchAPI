using Domain.OrganizationMatchNS.Queries;
using Domain.OrganizationMatchNS;
using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.OrganizationMatchNS.Queries;
using Domain.OrganizationNS;
using Domain.DeveloperNS;

namespace Domain.OrganizationMatchNS.Interfaces
{
    public interface IOrganizationMatchService
    {
        Task<bool> Add(OrganizationMatch match, CancellationToken cancellationToken);
        Task<IEnumerable<OrganizationMatch>> Get(OrganizationMatchQuery query);
        Task<bool> CheckForMatch(Guid developerUId, Guid OrganizationUId);
        Task<IEnumerable<Developer>> GetMyMatches(Guid organizationUid);
        Task<bool> Delete(int id);
        Task<IEnumerable<Developer>> GetDevelopersToMatch(Guid organizationUid, int stackId);
    }
}
