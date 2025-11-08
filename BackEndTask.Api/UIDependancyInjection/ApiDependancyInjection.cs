using Asp.Versioning;
using BackEndTask.Application.Profiles;
using BackEndTask.Application.Services.SetpsService.Commands.Handlers;
using BackEndTask.Data.Entites;
using BackEndTask.Persistance.DbContext;
using BackEndTask.Persistance.Helpers;
using BackEndTask.Persistance.Reposatories.Contracts;
using BackEndTask.Persistance.Reposatories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackEndTask.Api.UIDependancyInjection
{
    public static class ApiDependancyInjection
    {
        public static IServiceCollection  InjectApplicationService(this IServiceCollection serviceCollection)
        {
            // injection for api versionning
            serviceCollection.AddApiVersioning(option =>
            {
                option.DefaultApiVersion=new ApiVersion(1);
                option.AssumeDefaultVersionWhenUnspecified=true;
                option.ReportApiVersions=true;
                option.ApiVersionReader =new HeaderApiVersionReader("api-version");

            }).AddMvc()
            .AddApiExplorer(option =>
            {
                option.DefaultApiVersion=new ApiVersion(1);
                option.GroupNameFormat="'v'VVV";
                option.SubstituteApiVersionInUrl=true;
                option.AssumeDefaultVersionWhenUnspecified=true;
            });
            // inject medaitor
            serviceCollection.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblyContaining<SetpCommandHandler>();
            });

            // inject auto mapper
            serviceCollection.AddAutoMapper(typeof(ApplicationMapper).Assembly);

            return serviceCollection;
        }
        public static IServiceCollection InjectApplicationDbContextService(this IServiceCollection serviceCollection, IHostApplicationBuilder builder)
        {
            var isInMemory=(bool?)builder.Configuration.GetValue(typeof(bool),"IsInMemoryDb") ;

            var connectionString = builder.Configuration.GetConnectionString("Default");
            serviceCollection.AddDbContext<WorkFlowTrakDbContext>(option =>
            {
                if (isInMemory??false)
                {
                    option.UseInMemoryDatabase("InMemoDb");

                }
                else
                {
                    option.UseSqlServer(connectionString, option =>
                    {
                        option.CommandTimeout(120);
                    });
                }
            });
            

            return serviceCollection;
        }
        public   static IServiceCollection InjectReposatories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IReposatory<>), typeof(Reposatory<>));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<WorkFlowTrakDbContext>()
    .AddDefaultTokenProviders();
            services.AddHttpClient();
            return services;
        }
        public static IServiceCollection CollectDependancies(this IServiceCollection serviceCollection, IHostApplicationBuilder builder)
        {
            // applicaition Dependancies Collecting
            serviceCollection.InjectApplicationService();
            serviceCollection.InjectApplicationDbContextService(builder);
            serviceCollection.InjectReposatories();
          return  serviceCollection;

        }



        // seeding
        public static async Task<WebApplication> SeedData(this WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
              var roleManger=  scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var usermangerManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var logger=  scope.ServiceProvider.GetRequiredService<ILogger<WorkFlowTrakDbContextSeed>>();
              
               await WorkFlowTrakDbContextSeed.SeedAsync(roleManger, logger);
               await WorkFlowTrakDbContextSeed.SeedUsersAsync(usermangerManger, logger);
            }
            return app;
        }
    }


}
