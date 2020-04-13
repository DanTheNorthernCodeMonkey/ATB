using ATB.BackOffice.Domain;
using ATB.Data;
using ATB.Data.ReadModel;
using ATB.Data.Repository;
using ATB.FrontOffice.Domain;
using ATB.Infrastructure;
using ATB.Infrastructure.DataLayer;
using ATB.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ATB
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddScoped<IBeanAdvertCommand, BeanAdvertCommand>();
			services.AddScoped<IBeanAdvertManager, BeanAdvertManager>();
			services.AddScoped<IBeanAdvertRepository, BeanAdvertRepository>();
			services.AddScoped<IBeanOfTheDayQuery, BeanOfTheDayQuery>();

			services.AddScoped<IBeanAdvertReadModel, BeanAdvertReadModel>();
			services.AddTransient<IGateway, Gateway>();
			services.AddTransient<IFakeS3Service, FakeS3Service>();

			Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins,
					builder =>
						builder.WithOrigins(
								"http://localhost:3000",
								"http://localhost:3001",
								"http://localhost:3002",
								"http://localhost:3004",
								"http://localhost:3005",
								"http://localhost:3006")
							.AllowAnyMethod()
							.AllowAnyHeader());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseCors(MyAllowSpecificOrigins);

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}