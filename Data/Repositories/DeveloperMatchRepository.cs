using Data.Context;
using Domain.DeveloperMatchNS;
using Domain.DeveloperMatchNS.Interfaces;
using Domain.DeveloperMatchNS.Queries;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class DeveloperMatchRepository : IDeveloperMatchRepository
    {
        private readonly MatchDBContext _context;

        public DeveloperMatchRepository(MatchDBContext context)
        { 
            _context = context; 
        }

        public async Task<bool> Delete(int Id)
        {
            _context.Remove(Id);
            return true;
        }

        public async Task<IEnumerable<DeveloperMatch>> Get(DeveloperMatchQuery query, CancellationToken cancellationToken)
        {
            return await _context.DeveloperMatches.Where(x => (query.DeveloperUId != Guid.Empty && query.DeveloperUId == x.DeveloperUId) || (query.OrganizationUId != Guid.Empty && query.OrganizationUId == x.OrganizationUId)).ToListAsync(cancellationToken);
        }

        public async Task<bool> Create(DeveloperMatch match, CancellationToken cancellationToken)
        {
            await _context.AddAsync(match, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> CheckForMatch(Guid developerUid, Guid organizationUid)
        {
            var developerMatch = _context.DeveloperMatches.Where(x => x.DeveloperUId.Equals(developerUid) && x.OrganizationUId.Equals(organizationUid)).FirstOrDefault();
            var organizationMatch = _context.OrganizationMatches.Where(x => x.DeveloperUId.Equals(developerUid) && x.OrganizationUId.Equals(organizationUid)).FirstOrDefault();

            return developerMatch != null && organizationMatch != null ? true : false;

        }

        public async Task<IEnumerable<Guid>> GetMyApprovals(Guid developerUid)
        {
            return await _context.DeveloperMatches.Where(x => x.DeveloperUId == developerUid).Select(x => x.OrganizationUId).ToListAsync(new CancellationToken());
        }

        public async Task<IEnumerable<Guid>> GetMyMatches(Guid developerUid)
        {
            return  (from d in _context.DeveloperMatches
                             join o in _context.OrganizationMatches
                             on new { d.OrganizationUId, d.DeveloperUId } equals new { o.OrganizationUId, o.DeveloperUId }
                             where d.DeveloperUId == developerUid
                             select d.OrganizationUId).ToList();
        }
    }
}
