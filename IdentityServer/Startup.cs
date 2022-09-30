using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ��֤;

namespace IdentityServer
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

            //ע�����
            var idResources = new List<IdentityResource>
            {
              new IdentityResources.OpenId(), //����Ҫ��ӣ�������Ч�� scope ����
              new IdentityResources.Profile()
            };


            var section = Configuration.GetSection("SSOConfig");
            services.AddIdentityServer()
         .AddDeveloperSigningCredential()
           .AddInMemoryIdentityResources(idResources)
         .AddInMemoryApiResources(SSOConfig.GetApiResources(section))
         .AddInMemoryClients(SSOConfig.GetClients(section))
              .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);




           
         
         
          
          
          



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //  app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}