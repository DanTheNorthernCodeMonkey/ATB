using ATB.Common.BeanAdvert;
using ATB.Common.DataLayer;
using Microsoft.Extensions.DependencyInjection;

namespace ATB.Common
{
    public static class CommonServiceCollection
    {
        public static IServiceCollection AddCommonServiceCollection(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            services.AddScoped<IBeanAdvertReadModel, BeanAdvertReadModel>();
            services.AddTransient<IGateway, Gateway>();
            services.AddTransient<IFakeS3Service, FakeS3Service>();  
            

            return services;
        }
    }
}