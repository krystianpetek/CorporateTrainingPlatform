using GarageGenius.Modules.Customers.IntegrationTests.Helpers;
using GarageGenius.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace GarageGenius.Modules.Customers.IntegrationTests;

public class CustomersControllerTests
{
	private record class SignUpDto(string email, string password, string role);
	private record class SignInDto(string email, string password);
    private record class SignInResponseDto(Guid userId, Guid customerId, string accessToken, DateTime expiry, string role, Dictionary<string, List<string>> claims);

	private readonly HttpClient _client;
	private string _token = string.Empty;

	public CustomersControllerTests()
	{
		WebApplicationFactory<Program> mockWebApplication = new TestWebApplication<Program>();
		_client = mockWebApplication.CreateClient();
	}

	[Fact]
	public async Task TestAuthorization_ToChange()
	{
		var signUp = new SignUpDto("krystianpetek2@gmail.com", "Password!23","Administrator");
		var bodySignUp = JsonContent.Create(signUp);
		var responseSignUp = await _client.PostAsync("/users-module/users/sign-up", bodySignUp);
        responseSignUp.EnsureSuccessStatusCode();

        var signIn = new SignInDto("krystianpetek2@gmail.com", "Password!23");
        var bodySignIn = JsonContent.Create(signIn);
		var responseSignIn = await _client.PostAsync("/users-module/users/sign-in", bodySignIn);
        responseSignIn.EnsureSuccessStatusCode();

		var signInResponse = await responseSignIn.Content.ReadFromJsonAsync<SignInResponseDto>();
		_token = signInResponse.accessToken;

		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

		var response2 = await _client.GetAsync("/users-module/users/me");
		response2.EnsureSuccessStatusCode();

		Assert.Equal(HttpStatusCode.OK, responseSignIn.StatusCode);
	}
}