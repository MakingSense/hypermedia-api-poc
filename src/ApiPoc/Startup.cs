using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System;
using ApiPoc.Representations;

namespace ApiPoc
{
    public class Startup
    {
        //Another quick and dirty thing
        public static JsonOutputFormatter JsonOutputFormatter { get; private set; }

        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSubKey("AppSettings"));

            services.AddMvc().Configure<MvcOptions>(options =>
            {
                foreach (var formater in options.OutputFormatters.Select(x => x.Instance).OfType<JsonOutputFormatter>())
                {
                    formater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    formater.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    formater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    formater.SerializerSettings.Converters.Add(new RelValueConverter());
                    JsonOutputFormatter = formater;
                }

                //// TODO: It is not paying attention to System.Xml.Serialization attributes
                //options.AddXmlDataContractSerializerFormatter();
                //foreach (var formatter in options.OutputFormatters.Select(x => x.Instance).OfType<XmlDataContractSerializerOutputFormatter>())
                //{
                //    formater.WriterSettings.Indent = true;
                //}

                options.Filters.Add(new CustomExceptionFilterAttribute());
            });
            services.AddSingleton<IDatabase, FakeDatabase>();
        }

        public class RelValueConverter : JsonConverter
        {
            public override bool CanRead { get { return false; } }

            public override bool CanWrite { get { return true; } }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Rel);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var rel = (Rel)value;
                serializer.Serialize(writer, rel.ToRelString());
            }
        }
    }
}
