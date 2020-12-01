using Brreg.Http.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Brreg.Http
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBrregHttp(this IServiceCollection services)
        {
            services.AddHttpClient<IBrregHttpClient, BrregHttpClient>();
            return services;
        }
    }
}