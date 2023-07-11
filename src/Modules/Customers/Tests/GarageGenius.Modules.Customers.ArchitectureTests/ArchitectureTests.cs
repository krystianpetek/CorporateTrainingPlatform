using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace GarageGenius.Modules.Customers.ArchitectureTests;

public class ArchitectureTests
{
	private const string CustomersCore = "GarageGenius.Modules.Customers.Core";
	private const string CustomersApplication = "GarageGenius.Modules.Customers.Application";
	private const string CustomersInfrastructure = "GarageGenius.Modules.Customers.Infrastructure";
	private const string CustomersApi = "GarageGenius.Modules.Customers.Api";
	private const string CustomersShared = "GarageGenius.Modules.Customers.Shared";

	[Fact]
	public void CustomersCore_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { CustomersApplication, CustomersInfrastructure, CustomersApi, CustomersShared };

		// act
		var testResult = Types.InAssembly(Core.AssemblyCustomersCore.AssemblyReference)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void CustomersApplication_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { CustomersInfrastructure, CustomersApi };

		// act
		var testResult = Types.InAssembly(Application.AssemblyCustomersApplication.AssemblyReference)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}


	[Fact]
	public void CustomersInfrastructure_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { CustomersApi, CustomersShared };

		// act
		var testResult = Types.InAssembly(Infrastructure.AssemblyCustomersInfrastructure.AssemblyReference)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void CustomersApi_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { CustomersCore, CustomersShared };

		// act
		var testResult = Types.InAssembly(Api.AssemblyCustomersApi.AssemblyReference)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void CustomersShared_Should_Not_HaveDependencyOnAnyCustomersProject()
	{
		// arrange
		string[] projects = { CustomersCore, CustomersApplication, CustomersInfrastructure, CustomersApi };

		// act
		var testResult = Types.InAssembly(Shared.AssemblyCustomersShared.AssemblyReference)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Handlers_Should_Have_DependencyOnDomain()
	{
		// arrange
		var assembly = Application.AssemblyCustomersApplication.AssemblyReference;

		// act
		var testResult = Types.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("Handler")
			.Should()
			.HaveDependencyOn(CustomersCore)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Controllers_Should_DependencyOnApplication()
	{
		// arrange
		var assembly = Api.AssemblyCustomersApi.AssemblyReference;

		//act
		var testResult = Types.InAssembly(assembly)
			.That()
			.DoNotHaveNameStartingWith("BaseController")
			.And()
			.HaveNameEndingWith("Controller")
			.Should()
			.HaveDependencyOn(CustomersApplication)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}
}
// TODO - Add tests for Api and Shared projects, check other projects for dependencies on this module