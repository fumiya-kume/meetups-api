using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using meetupsApi.Models;
using meetupsApi.Tests.Domain.Repository;
using meetupsApi.Tests.Domain.Usecase;
using meetupsApi.Tests.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace meetupsApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MeetupsApiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("meetupsApiContext")));

            // Domain Layer
            services.AddTransient<IConnpassReadOnlyWebsiteDataRepository, ConnpassReadOnlyWebsiteWebsiteDataRepository>();
            services.AddTransient<IConnpassDataStore, ConnpassDatastore>();
            services.AddTransient<IRefreshConnpassDataUsecase, RefreshConnpassDataUsecase>();

            // InfraLayer
            services.AddTransient<IConnpassDatabaseRepository, ConnpassDatabaseRepository>();
            services.AddTransient<MeetupsApiContext>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}