using System;
using Newtonsoft.Json;

namespace APIClient
{
	internal static class UtilExtensions
	{
		public static string ToJson(this object data)
		{
			return JsonConvert.SerializeObject(data, Formatting.Indented);
		}
		public static void ToConsole(this string data)
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("========================================");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(data);

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("========================================");

			Console.ResetColor();

		}
	}
}