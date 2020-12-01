using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Brreg.Controllers
{
    public class OrganizationController : Controller
    {
        // 1. Fetch org from brreg using IBrregHttpClient
        // 2. Implement IMemoryCache and clear cache after 30 seconds, so we dont refetch data we already have
        // 3. Validate and return badrequest if orgnumber is less then 9 digits
        // 4. Api output should contain organizationNumber and name
        
        //https://data.brreg.no/enhetsregisteret/api/enheter/919300388
        // GET
        public Task<IActionResult> Index(string org)
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}