using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Newtonsoft.Json.Serialization;

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
                foreach (var formater in options.OutputFormatters.Select(x => x.Instance).OfType<JsonOutputFormatter>())
                {
                    formater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    formater.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    formater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }

                //// TODO: It is not paying attention to System.Xml.Serialization attributes 
                //options.AddXmlDataContractSerializerFormatter();
                //foreach (var formater in options.OutputFormatters.Select(x => x.Instance).OfType<XmlDataContractSerializerOutputFormatter>())
                //{
                //    formater.WriterSettings.Indent = true;
                //}

            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
