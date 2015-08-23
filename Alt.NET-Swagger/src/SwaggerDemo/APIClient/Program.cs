using System;
using Microsoft.Rest.TransientFaultHandling;

namespace APIClient
{
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("Press any key...");
			Console.ReadKey();

			var endpointUrl = "http://swagger.localtest.me";


			//SimpleClientInvocation(new Uri(endpointUrl));
			//Console.WriteLine("Press any key...");
			//Console.ReadKey();

			//ClientInvocationWithRetryStrategy(new Uri(endpointUrl));

			Console.WriteLine("Press any key to quit...");
			Console.ReadKey();
		}

		private static void SimpleClientInvocation(Uri targetUrl)
		{
			Console.WriteLine("Calling : SimpleClientInvocation");
			Console.WriteLine();
			//using ( var altCli = new AltNETDemo(targetUrl) )
			//{
			//	var createdUser = altCli.User.CreateUserAsync(new UserDTO { Id = 25, Company = "SkyNet", Name = "Arnold Sch-war-ugh-err", Tag = "T-800", PreferredLanguage = "Asm-2020", })
			//		.Result;

			//	var freshMan = altCli.User.GetUserById(25);
			//	freshMan.ToJson().ToConsole();

			//	var terminated = altCli.User.DeletUserById(43);

			//	var usersData = altCli.User.ListAllUsersAsync(4)
			//		.Result
			//		.ToJson();

			//	Console.ForegroundColor = ConsoleColor.Yellow;
			//	Console.WriteLine(usersData);
			//	Console.ResetColor();
			//}
		}

		private static void ClientInvocationWithRetryStrategy(Uri targetUrl)
		{
			Console.WriteLine("Calling : ClientInvocationWithRetryStrategy");
			Console.WriteLine();

			var retryPolicy = new RetryPolicy<HttpStatusCodeErrorDetectionStrategy>(
				new ExponentialBackoffRetryStrategy("Alt.Net RetryStrategy", 
				retryCount:5,
				minBackoff:TimeSpan.Zero, 
				maxBackoff: TimeSpan.FromSeconds(10),
				deltaBackoff:TimeSpan.FromSeconds(1),
				firstFastRetry: true));


			//using ( var myClient = new AltNETDemo(targetUrl) )
			//{
			//	myClient.SetRetryPolicy(retryPolicy);

			//	var companyData = myClient.Company.GetListAsync(4).Result;

			//	companyData.ToJson().ToConsole();
			//}
		}
	}
}