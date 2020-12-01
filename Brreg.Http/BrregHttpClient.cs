using System;
using System.Net.Http;
using System.Threading.Tasks;
using Brreg.Http.Abstractions;
using Brreg.Http.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Brreg.Http
{
    public class BrregHttpClient : IBrregHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public BrregHttpClient(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        
        public Task<OrganizationModel> FetchOrganizationAsync(string orgNumber)
        {
            return _cache.GetOrCreateAsync(orgNumber, async ce =>
            {
                ce.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30);
                var response = await _httpClient.GetAsync(ApplicationConstants.BRREG_ORG_URL + orgNumber);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<OrganizationModel>(await response.Content.ReadAsStringAsync());
                }
                throw new HttpRequestException("Failed to fetch organization");
            });
        }
    }
}