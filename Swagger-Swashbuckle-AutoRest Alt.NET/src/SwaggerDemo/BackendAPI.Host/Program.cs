using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Swashbuckle.Application;

namespace BackendAPI.Host
{
	class Program
	{
		static void Main()
		{
			var address = "http://swagger.localtest.me";
			var config = ConfigureWeb();

			using ( var apiApp = WebApp.Start(address, (app) => { app.UseWebApi(config); }) )
			{
				Console.WriteLine("Website started!");
				while ( true )
				{
					Task.Delay(10000, CancellationToken.None);
				}
			}

		}

		private static HttpConfiguration ConfigureWeb()
		{
			var config = new HttpConfiguration(); // WebAPI config
			config.MapHttpAttributeRoutes();
			config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();


			//config.EnableSwagger(c => {
			//	c.SingleApiVersion("v1", "Alt.NET Demo ")
			//		.Description("Swagger API Demo for ALT.NET Melbourne")
			//		.Contact(cc => cc.Url("http://swagger.io"));
			//	//c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Docs.xml");
			//})
			//.EnableSwaggerUi(c => { var assembly = typeof(UserController).Assembly;});

			return config;
		}
	}
}
