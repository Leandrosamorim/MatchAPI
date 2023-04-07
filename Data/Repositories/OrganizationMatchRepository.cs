using Data.Context;
using Domain.OrganizationMatchNS;
using Domain.OrganizationMatchNS.Interfaces;
using Domain.OrganizationMatchNS.Queries;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrganizationMatchRepository : IOrganizationMatchRepository
    {
        private readonly MatchDBContext _context;

        public OrganizationMatchRepository(MatchDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckForMatch(Guid developerUid, Guid organizationUid)
        {
            var developerMatch = _context.DeveloperMatches.Where(x => x.DeveloperUId.Equals(developerUid) && x.OrganizationUId.Equals(organizationUid)).FirstOrDefault();
            var organizationMatch = _context.OrganizationMatches.Where(x => x.DeveloperUId.Equals(developerUid) && x.OrganizationUId.Equals(organizationUid)).FirstOrDefault();

            return developerMatch != null && organizationMatch != null ? true : false;
        }

        public async Task<bool> Create(OrganizationMatch match, CancellationToken cancellationToken)
        {
            try
            {
                await _context.OrganizationMatches.AddAsync(match, cancellationToken);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var match = await _context.OrganizationMatches.Where(x => x.Id == id).FirstOrDefaultAsync();
                _context.OrganizationMatches.Remove(match);
                _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<OrganizationMatch>> Get(OrganizationMatchQuery query, CancellationToken cancellationToken)
        {
            return await _context.OrganizationMatches.Where(x => (query.OrganizationUId != Guid.Empty && query.OrganizationUId == x.OrganizationUId) || (query.DeveloperUId != Guid.Empty && query.DeveloperUId == x.DeveloperUId)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetMyApprovals(Guid organizationUId)
        {
            return await _context.OrganizationMatches.Where(x => x.OrganizationUId.Equals(organizationUId)).Select(x => x.DeveloperUId).ToListAsync(new CancellationToken());
        }

        public async Task<IEnumerable<Guid>> GetMyMatches(Guid organizationUId)
        {
            return await (from o in _context.OrganizationMatches
                   join d in _context.DeveloperMatches
                   on new { o.OrganizationUId, o.DeveloperUId } equals new { d.OrganizationUId, d.DeveloperUId }
                   where o.OrganizationUId == organizationUId
                   select d.DeveloperUId).ToListAsync(new CancellationToken());
        }
    }
}
