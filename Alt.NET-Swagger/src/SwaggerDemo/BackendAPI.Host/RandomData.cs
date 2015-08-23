using System;

namespace BackendAPI.Host
{
	public static class RandomData
	{
		private static readonly Random _rnd = new Random((int) DateTime.UtcNow.Ticks % int.MaxValue);


		public static string[] Names = { "Angelina Jolie","Ben Affleck","Brad Pitt","Bruce Willis","Catherine Zeta-Jones","Chris Hemsworth","Denzel Washington","Eddie Murphy","George Clooney","Jason Statham","Johnny Depp","Justin Bieber","Leonardo DiCaprio","Matt Damon","Robert De Niro","Shia LaBeouf","Sylvester Stallone","Tom Cruise","Tom Hanks","Tommy Lee Jones",};

		public static string[] Tags = { "Iron Man", "Captain America", "Hulk", "Batman", "Magneto", "Professor X", "Raccoon", "I am Groot!", "Catwoman", "Dr. Manhattan" };

		public static string[] Companies = { "Microsoft", "Google", "Kiandra IT", "Drawboard", "IBM", "Readify", "Amazon", "PayPal", "Dropbox", "Tesla" };

		public static string[] Langs = { ".NET", "Clojure", "Go", "Java", "JavaScript", "Node.JS", "Perl", "PHP", "Python", "Ruby" };


		public static TResult Get<TResult>(this TResult[] input)
		{
			return input[_rnd.Next(0, input.Length)];
		}
	}
}