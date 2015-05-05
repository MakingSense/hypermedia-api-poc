using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace ApiPoc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
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
                //foreach (var formatter in options.OutputFormatters.Select(x => x.Instance).OfType<XmlDataContractSerializerOutputFormatter>())
                //{
                //    formater.WriterSettings.Indent = true;
                //}
            });
        }
    }
}
