using System.Threading.Tasks;
using Brreg.Http_.Models;

namespace Brreg.Http_.Abstractions
{
    public interface IBrregHttpClient
    {
        Task<OrganizationModel> FetchOrganizationAsync(string orgNumber);
    }
}