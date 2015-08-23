using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BackendAPI.Host
{
	/// <summary>
	///     Controller to get awesome developers from great companies
	/// </summary>
	[RoutePrefix("api/user")]
	public class UserController: ApiController
	{
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

			return new UserDTO {
				Id = id,
				Name = RandomData.Names.Get(),
				Tag = RandomData.Tags.Get(),
				Company = RandomData.Companies.Get(),
				PreferredLanguage = RandomData.Langs.Get()
			};
		}

		/// <summary>
		///     Create a new user
		/// </summary>
		/// <param name="user"> User model.</param>
		/// <returns>Result of user creation - 201 created or </returns>
		/// <response code="201">User was added to the database</response>
		/// <response code="400">Incorrect data format</response>
		/// <response code="500">We are sorry, something went wrong.</response>
		[HttpPost, Route("")]
		public IHttpActionResult CreateUser(UserDTO user)
		{
			return user.Id > 10
				? (IHttpActionResult) Created($"http://swagger.localtest.me/api/user/{user.Id}", user)
				: BadRequest();
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


	[RoutePrefix("api/company")]
	public class CompanyController: ApiController
	{
		[HttpGet, Route("{id}")]
		public CompanyDTO GetCompany(string companyId)
		{
			throw new NotImplementedException();
		}

		[HttpPost, Route("")]
		public IHttpActionResult CreateCompany([FromBody]CompanyDTO data)
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
			throw new NotImplementedException();
		}

		[HttpGet, Route("{companyId}/projects/")]
		public ProjectDTO[] GetCompanyProjects(string companyId)
		{
			throw new NotImplementedException();
		}

	}


	[RoutePrefix("api/project")]
	public class ProjectController: ApiController
	{

		[HttpGet, Route("{projectId}")]
		public ProjectDTO GetProjectById(Guid projectId)
		{
			throw new NotImplementedException();
		}

		[HttpGet, Route("{projectId}")]
		public UserDTO[] GetProjectDevelopers(Guid projectId)
		{
			throw new NotImplementedException();
		}

	}
}