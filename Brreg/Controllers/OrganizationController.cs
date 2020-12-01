using Microsoft.AspNetCore.Mvc;

namespace Brreg.Controllers
{
    public class OrganizationController : Controller
    {
        //https://data.brreg.no/enhetsregisteret/api/enheter/919300388
        // GET
        public IActionResult Index(string org)
        {
            return Ok();
        }
    }
}