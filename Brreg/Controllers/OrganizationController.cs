using System;
using System.Threading.Tasks;
using Brreg.Http.Abstractions;
using Brreg.Http.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Brreg.Controllers
{
    [Route("[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IBrregHttpClient _brregHttpClient;
        private readonly IMemoryCache _cache;

        // 1. Fetch org from brreg using IBrregHttpClient
        // 2. Implement IMemoryCache and clear cache after 30 seconds, so we dont refetch data we already have
        // 3. Return NotFound if no result 
        // 4. Validate and return badrequest if orgnumber is less then 9 digits
        // 5. Api output should contain organizationNumber and name
        public OrganizationController(IBrregHttpClient brregHttpClient, IMemoryCache cache)
        {
            _brregHttpClient = brregHttpClient;
            _cache = cache;
        }
        //https://data.brreg.no/enhetsregisteret/api/enheter/919300388
        // GET
        [HttpGet("{org}")]
        public async Task<IActionResult> Index(string org)
        {
            var result = await _brregHttpClient.FetchOrganizationAsync(org);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(new { Name = result.Navn, OrganizationNumber = result.Organisasjonsnummer });
        }
    }
}