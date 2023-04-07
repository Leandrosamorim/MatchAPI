using Domain.OrganizationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Interfaces
{
    public interface IDeveloperHttpService
    {
        public Task<IEnumerable<Developer>> GetByStack(int stackId);
        public Task<IEnumerable<Developer>> GetDevelopersByUId(IEnumerable<Guid> uids);
    }
}
