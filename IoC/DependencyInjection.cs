using Data.Context;
using Data.Repositories;
using Domain.DeveloperMatchNS.Interfaces;
using Domain.DeveloperMatchNS.Services;
using Domain.DeveloperNS.HttpService;
using Domain.DeveloperNS.Interfaces;
using Domain.OrganizationMatchNS.Interfaces;
using Domain.OrganizationMatchNS.Service;
using Domain.OrganizationNS.HttpService;
using Domain.OrganizationNS.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MatchDBContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IOrganizationHttpService, OrganizationHttpService>();
            services.AddScoped<IOrganizationMatchRepository, OrganizationMatchRepository>();
            services.AddScoped<IOrganizationMatchService, OrganizationMatchService>();
            services.AddTransient<IDeveloperHttpService, DeveloperHttpService>();
            services.AddScoped<IDeveloperMatchRepository, DeveloperMatchRepository>();
            services.AddScoped<IDeveloperMatchService, DeveloperMatchService>();

            return services;
        }
    }
}
