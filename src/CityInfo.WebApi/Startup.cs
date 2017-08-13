using System.Buffers;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CityInfo.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(ConfigureNamingStrategy)
                .AddMvcOptions(ConfigureOutputFormatters);
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

        private void ConfigureOutputFormatters(MvcOptions options)
        {
            options.OutputFormatters.Clear();

            options.OutputFormatters.Add(
                new JsonOutputFormatter(new JsonSerializerSettings(), ArrayPool<char>.Shared)
            );

            options.OutputFormatters.Add(
                new XmlDataContractSerializerOutputFormatter()
            );
        }

        private void ConfigureNamingStrategy(MvcJsonOptions options)
        {
            var contractResolver = options.SerializerSettings?.ContractResolver as DefaultContractResolver;

            if (contractResolver != null)
            {
                contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
            }
        }

        #endregion
    }
}
