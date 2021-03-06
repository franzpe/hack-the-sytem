using System.Linq;
using API.Core.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.Core.Platform.Validators;
using Microsoft.Extensions.Options;
using API.Core.Services;
using IdentityServer4.Stores;

namespace API.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "Server API for hackathon",
                    Title = "API.Core",
                    Version = "v1"
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.Configure<ContractstoreDatabaseSettings>(
            Configuration.GetSection(nameof(ContractstoreDatabaseSettings)));

            services.AddSingleton<IContractstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ContractstoreDatabaseSettings>>().Value);
            services.AddSingleton<IContractService, ContractService>();

            services.Configure<UserstoreDatabaseSettings>(
                Configuration.GetSection(nameof(UserstoreDatabaseSettings)));

            services.AddSingleton<IUserstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<UserstoreDatabaseSettings>>().Value);
            services.AddSingleton<IUserProfileService, UserProfileService>();

            services.AddIdentityServer()
                .AddInMemoryCaching()
                .AddClientStore<InMemoryClientStore>()
                .AddResourceStore<InMemoryResourcesStore>();

            services.AddSingleton<IUserstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<UserstoreDatabaseSettings>>().Value);
            services.AddSingleton<IUserProfileService, UserProfileService>();

            services.AddTransient<IUserValidator, UserValidator>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Core V1");
            });

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=contract}/{action=create}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
