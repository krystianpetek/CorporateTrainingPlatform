using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using GarageGenius.WebApi;

namespace GarageGenius.Modules.Customers.IntegrationTests;

public class CustomersModuleTests
{
	private record class SignInDto(string email, string password);
	private record class SignInResponseDto(Guid userId, Guid customerId, string accessToken, DateTime expiry, string role, Dictionary<string, List<string>> claims);

	private readonly HttpClient _client;
	public CustomersModuleTests()
	{
		var factory = new WebApplicationFactory<Program>();
		_client = factory.CreateClient();
	}

	[Fact]
	public async Task Test1()
	{
		var signIn = new SignInDto("krystianpetek2@gmail.com", "Password!23");
		var body = JsonContent.Create(signIn);
		var response = await _client.PostAsync("/users-module/users/sign-in", body);
		response.EnsureSuccessStatusCode();

		var signInResponse = await response.Content.ReadFromJsonAsync<SignInResponseDto>();

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}