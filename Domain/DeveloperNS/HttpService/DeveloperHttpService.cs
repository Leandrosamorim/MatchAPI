using Domain.DeveloperNS.Interfaces;
using Domain.OrganizationNS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.HttpService
{
    public class DeveloperHttpService : IDeveloperHttpService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public DeveloperHttpService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<Developer>> GetByStack(int stackId)
        {
            var url = _config.GetSection("DeveloperAPI").Value.ToString() + $"api/Developer?stackId={stackId}";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Developer>>();
                return json;
            }

            return default;
        }

        public async Task<IEnumerable<Developer>> GetDevelopersByUId(IEnumerable<Guid> uids)
        {
            var url = "api/Developer/contact?UId=";
            var param = string.Join("&UId=", uids);
            var response = await _client.GetAsync(url + param);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Developer>>();
                return json;
            }

            return default;
        }
    }
}
