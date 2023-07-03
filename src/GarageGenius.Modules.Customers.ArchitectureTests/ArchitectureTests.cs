using FluentAssertions;
using NetArchTest.Rules;

namespace GarageGenius.Modules.Customers.ArchitectureTests;

public class ArchitectureTests
{
	private const string ModuleName = "GarageGenius.Modules.Customers";
	private const string Core = ModuleName + ".Core";
	private const string Application = ModuleName + ".Application";
	private const string Infrastructure = ModuleName + ".Infrastructure";
	private const string Api = ModuleName + ".Api";
	private const string Shared = ModuleName + ".Shared";

	[Fact]
	public void Core_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { Application, Infrastructure, Api };

		// act
		var testResult = Types.InCurrentDomain()
			.That()
			.ResideInNamespace(Core)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Application_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { Infrastructure, Api };

		// act
		var testResult = Types.InCurrentDomain()
			.That()
			.ResideInNamespace(Application)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}


	[Fact]
	public void Infrastructure_Should_Not_HaveDependencyOnProjectsFromList()
	{
		// arrange
		string[] projects = { Api, "GarageGenius.Shared.Infrastructure" };

		// act
		var testResult = Types.InCurrentDomain()
			.That()
			.ResideInNamespace(Infrastructure)
			.ShouldNot()
			.HaveDependencyOnAny(projects)
			.GetResult();

		// assert
		testResult.IsSuccessful.Should().BeTrue();
	}
}
// TODO - Add tests for Api and Shared projects, check other projects for dependencies on this module