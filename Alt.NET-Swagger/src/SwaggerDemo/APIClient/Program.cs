using System;
using Newtonsoft.Json;

namespace APIClient
{
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("Press any key to start");
			Console.ReadKey();

			var endpointUrl = "http://swagger.localtest.me";


			SimpleClientInvocation(new Uri(endpointUrl));


			Console.WriteLine("Press any key to finish");
			Console.ReadKey();
		}


		private static void SimpleClientInvocation(Uri targetUrl)
		{
			//using (var myClient = new AltNETDemo(targetUrl))
			//{
			//	var usersData = myClient.User.ListAllUsersAsync(3)
			//		.Result
			//		.ToJson();

			//	Console.ForegroundColor = ConsoleColor.Yellow;
			//	Console.WriteLine(usersData);
			//	Console.ResetColor();
			//}
		}
	}

	/// <summary>
	/// Helper for demo
	/// </summary>
	internal static class JsonExts
	{
		/// <summary>
		/// Serialize to indented Json 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string ToJson(this object data)
		{
			return JsonConvert.SerializeObject(data, Formatting.Indented);
		}
	}
}