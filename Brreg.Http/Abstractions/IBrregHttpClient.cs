using System.Threading.Tasks;
using Brreg.Http.Models;

namespace Brreg.Http.Abstractions
{
    public interface IBrregHttpClient
    {
        Task<OrganizationModel> FetchOrganizationAsync(string orgNumber);
    }
}