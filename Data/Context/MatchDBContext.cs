using Domain.DeveloperMatchNS;
using Domain.OrganizationMatchNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class MatchDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MatchDBContext(DbContextOptions<MatchDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<OrganizationMatch> OrganizationMatches { get; set; }
        public virtual DbSet<DeveloperMatch> DeveloperMatches { get; set; }
    }
}
