using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc;

namespace ApiPoc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                //Quick and dirty patch to have json indentation
                ((Microsoft.AspNet.Mvc.JsonOutputFormatter)options.OutputFormatters[2].Instance).SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.AddXmlDataContractSerializerFormatter();
                ((Microsoft.AspNet.Mvc.XmlDataContractSerializerOutputFormatter)options.OutputFormatters[3].Instance).WriterSettings.Indent = true;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
