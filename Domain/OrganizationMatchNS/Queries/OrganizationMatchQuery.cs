using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationMatchNS.Queries
{
    public class OrganizationMatchQuery
    {
        public Guid OrganizationUId { get; set; }
        public Guid DeveloperUId { get; set; }
    }
}
