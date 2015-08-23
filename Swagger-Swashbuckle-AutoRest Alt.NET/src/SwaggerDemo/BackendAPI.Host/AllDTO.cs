using System;

namespace BackendAPI.Host
{
	/// <summary>
	///     User data structure to return from API
	/// </summary>
	public class UserDTO
	{
		/// <summary>
		///     User Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Full name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     User's Secret tag
		/// </summary>
		public string Tag { get; set; }

		/// <summary>
		///     Current Employer
		/// </summary>
		public string Company { get; set; }

		/// <summary>
		///     Preferred programming language
		/// </summary>
		public string PreferredLanguage { get; set; }
	}

	/// <summary>
	///     DTO to describe company, it's employees and active projects
	/// </summary>
	public class CompanyDTO
	{
		public string Id { get; set; }
		public string CompanyName { get; set; }
		public string Location { get; set; }
		public string StockTicker { get; set; }
		public UserDTO[] Employees { get; set; }
		public ProjectDTO[] ActiveProjects { get; set; }
	}

	public class ProjectDTO
	{
		public Guid Id { get; set; }
		public string ProjectName { get; set; }
		public string DevEnvironmentUrl { get; set; }
		public string ProductionEnvironmentUrl { get; set; }
		public UserDTO[] MainDevelopers { get; set; }
		public UserDTO[] Testers { get; set; }
	}
}