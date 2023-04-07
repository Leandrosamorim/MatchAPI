using Domain.OrganizationNS.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.OrganizationNS.HttpService
{
    public class OrganizationHttpService : IOrganizationHttpService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public OrganizationHttpService(HttpClient client, IConfiguration config) 
        { 
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            var url = _config.GetSection("OrganizationAPI").Value.ToString() + "api/Organization/all";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Organization>>();
                return json;
            }

            return default;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsByUId(IEnumerable<Guid> uids)
        {
            var url = _config.GetSection("OrganizationAPI").Value.ToString() + "api/Organization?UId=";
            var param = string.Join("&UId=", uids);
            var response = await _client.GetAsync(url + param);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Organization>>();
                return json;
            }

            return default;
        }
    }
}
