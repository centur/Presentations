using System;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Http.Description;

namespace BackendAPI.Host
{
	/// <summary>
	///     Controller to get awesome developers from great companies
	/// </summary>
	[RoutePrefix("api/user")]
	public class UserController: ApiController
	{
		private readonly MemoryCache _cache = MemoryCache.Default;
		private readonly CacheItemPolicy _cachePolicy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromSeconds(60) };

		/// <summary>
		///     Get specific user by Id
		/// </summary>
		/// <remarks>
		///     This operation is expensive to call as it'll lookup in multiple databases.
		/// </remarks>
		/// <param name="id"> Id of the user to find.</param>
		/// <returns>User details if user has been found in the database.</returns>
		/// <response code="200">User found.</response>
		/// <response code="400">Id parameter has invalid format or out of expected range.</response>
		/// <response code="404">User with passed Id doesn't exists.</response>
		[HttpGet, Route("{id}")]
		public UserDTO GetUserById(int id)
		{
			if ( id == 42 )
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			if ( id > 100 )
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			if ( _cache.Contains($"user-{id}") )
			{
				return _cache.Get($"user-{id}") as UserDTO;
			}


			return new UserDTO {
				Id = id,
				Name = RandomData.Names.Get(),
				Tag = RandomData.Tags.Get(),
				Company = RandomData.Companies.Get(),
				PreferredLanguage = RandomData.Langs.Get()
			};
		}

		/// <summary>
		///     Create a new user, user Id must be > 10
		/// </summary>
		/// <param name="user"> User model.</param>
		/// <returns>Result of user creation - 201 created or 400 Bad request.</returns>
		/// <response code="201">User was added to the database</response>
		/// <response code="400">Incorrect data format</response>
		/// <response code="409">User with this Id already exists</response>
		/// <response code="500">We are sorry, something went wrong.</response>
		[HttpPost, Route("")]
		//[ResponseType(typeof(UserDTO))]
		public IHttpActionResult CreateUser(UserDTO user)
		{
			if ( _cache.Contains($"user-{user.Id}") )
			{
				return Conflict();
			}

			_cache.Add(new CacheItem($"user-{user.Id}", user), _cachePolicy);


			return user.Id > 10
				? (IHttpActionResult) Created($"http://swagger.localtest.me/api/user/{user.Id}", user)
				: BadRequest();
		}

		/// <summary>
		///     Update user. User Id must be > 10
		/// </summary>
		/// <param name="id">User Id to update</param>
		/// <param name="user">Updated user model.</param>
		/// <returns>Result of update operation</returns>
		/// <response code="201">User was updated in the database</response>
		/// <response code="400">Incorrect data format</response>
		/// <response code="500">We are sorry, something went wrong.</response>
		[HttpPut, Route("{id}")]
		public IHttpActionResult UpdateUser([FromUri]int id, [FromBody]UserDTO user)
		{
			_cache.Set(new CacheItem($"user-{id}", user), _cachePolicy);

			return user.Id > 10 ? (IHttpActionResult) Ok() : BadRequest();
		}

		/// <summary>
		///     Delete user. User Id must be > 10
		/// </summary>
		/// <param name="id"> Id of the user to delete</param>
		/// <returns>Result of user creation - 201 created or </returns>
		/// <response code="200">User was deleted.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="500">We are sorry, something went wrong.</response>
		[HttpDelete, Route("{id}")]
		public IHttpActionResult DeletUserById(int id)
		{
			_cache.Remove($"user-{id}");
			return id > 10 ? (IHttpActionResult) Ok() : BadRequest();
		}


		/// <summary>
		///     List all users available for contracts
		/// </summary>
		/// <param name="pageSize">Number of records to return</param>
		/// <returns>Collection of all users with personal details, current employer and preffered programming languages</returns>
		[HttpGet, Route("list")]
		public UserDTO[] ListAllUsers(int pageSize = 10)
		{
			var data = Enumerable.Range(1, pageSize)
				.Select(i =>
					new UserDTO {
						Id = i*13,
						Name = RandomData.Names.Get(),
						Tag = RandomData.Tags.Get(),
						Company = RandomData.Companies.Get(),
						PreferredLanguage = RandomData.Langs.Get()
					})
				.ToArray();

			return data;
		}

		/// <summary>
		///     List all users for the specific project
		/// </summary>
		/// <param name="projectId">Project ID</param>
		/// <returns>Collection of users who works on specific project</returns>
		[HttpGet, Route("list/byproject/{projectId}")]
		public UserDTO[] ListAllUsersByProject(Guid projectId)
		{
			var data = Enumerable.Range(1, 5)
				.Select(i =>
					new UserDTO {
						Id = i*27,
						Name = RandomData.Names.Get(),
						Tag = RandomData.Tags.Get(),
						Company = RandomData.Companies.Get(),
						PreferredLanguage = RandomData.Langs.Get()
					})
				.ToArray();

			return data;
		}
	}


	/// <summary>
	/// Sample company controller to display multiple items in swagger UI
	/// </summary>
	[RoutePrefix("api/company")]
	public class CompanyController: ApiController
	{
		private readonly MemoryCache _cache = MemoryCache.Default;


		[HttpGet, Route("{id}")]
		public CompanyDTO GetCompany(string companyId)
		{
			throw new NotImplementedException();
		}

		[HttpPost, Route("")]
		public IHttpActionResult CreateCompany([FromBody] CompanyDTO data)
		{
			throw new NotImplementedException();
		}

		[HttpPut, Route("{id}")]
		public CompanyDTO UpdateCompany(string companyId, [FromBody] CompanyDTO updatedData)
		{
			throw new NotImplementedException();
		}

		[HttpDelete, Route("{id}")]
		public IHttpActionResult DeleteCompany(string companyId)
		{
			throw new NotImplementedException();
		}

		[HttpGet, Route("")]
		public CompanyDTO[] GetList(int pageSize = 10)
		{
			// Imitating server errors
			var cacheKey = "company-retry-counter";
			int counter = 1;

			if ( _cache.Contains(cacheKey) )
				counter = (int) _cache[cacheKey];
			else
				_cache[cacheKey] = counter;


			if ( counter < 4 )
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine($"Accessing Company.List. Attempt:{counter}");
				_cache["company-retry-counter"] = ++counter;
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Accessing Company.List. Attempt:{counter}");

			return new[]
			{
				new CompanyDTO
				{
					Id = "17", StockTicker = "DPDF", CompanyName = "Drawboard",Location = "Melbourne, Australia",
					Employees = Enumerable.Range(1, 4).Select(i => new UserDTO { Id = i*27, Name = RandomData.Names.Get(), Tag = RandomData.Tags.Get(),Company = RandomData.Companies.Get(),PreferredLanguage = RandomData.Langs.Get()}).ToArray()},

				new CompanyDTO {
					Id = "41", StockTicker = "KIAN",CompanyName = "KiandraIT",Location = "Melbourne, Australia",
					Employees = Enumerable.Range(1, 4).Select(i => new UserDTO { Id = i*17, Name = RandomData.Names.Get(), Tag = RandomData.Tags.Get(),Company = RandomData.Companies.Get(),PreferredLanguage = RandomData.Langs.Get()}).ToArray()}
			};


		}

		[HttpGet, Route("{companyId}/projects/")]
		public ProjectDTO[] GetCompanyProjects(string companyId)
		{
			throw new NotImplementedException();
		}
	}


	/// <summary>
	/// Sample Project controller to display multiple items in swagger UI
	/// </summary>
	[RoutePrefix("api/project")]
	public class ProjectController: ApiController
	{
		[HttpGet, Route("{projectId}")]
		public ProjectDTO GetProjectById(Guid projectId)
		{
			throw new NotImplementedException("Hey we need to implement GetProjectById method first");
		}

		[HttpGet, Route("{projectId}/users")]
		public UserDTO[] GetProjectDevelopers(Guid projectId)
		{
			throw new NotImplementedException("Hey we need to implement GetProjectDevelopers method first");
		}
	}
}