using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.Core.Orchestrators;
using API.Core.Platform.Validators;
using API.Core.Models;
using Microsoft.Extensions.Options;
using API.Core.Services;

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
            services.Configure<ContractstoreDatabaseSettings>(
            Configuration.GetSection(nameof(ContractstoreDatabaseSettings)));

            services.AddSingleton<IContractstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ContractstoreDatabaseSettings>>().Value);
            services.AddSingleton<ContractService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Core", Version = "v1" });
            });

            services.AddTransient<IDraftOrchestrator, DraftOrchestrator>();
            services.AddTransient<IDraftValidator, DraftValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Core v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
