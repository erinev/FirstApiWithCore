﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
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
                app.UseExceptionHandler();
            }

            app.UseMvc();
        }

        #region Private Functions

        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    var contractResolver = options.SerializerSettings?.ContractResolver as DefaultContractResolver;

                    if (contractResolver != null)
                    {
                        contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
                    }
                });
        }

        #endregion
    }
}
