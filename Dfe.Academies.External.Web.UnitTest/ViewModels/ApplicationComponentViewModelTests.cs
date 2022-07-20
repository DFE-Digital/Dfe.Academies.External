using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationComponentViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void ApplicationComponentViewModel___PropertyCheck___Success()
	{
		// arrange
		string name = Fixture.Create<string>();
		string uri = Fixture.Create<string>();
		Status status = Fixture.Create<Status>();

		var applicationComponentViewModel = new ApplicationComponentViewModel(name, uri)
		{
			Status = status
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponentViewModel, Is.Not.Null);
		Assert.That(applicationComponentViewModel.Name, Is.EqualTo(name));
		Assert.That(applicationComponentViewModel.URI, Is.EqualTo(uri));
		Assert.That(applicationComponentViewModel.Status, Is.EqualTo(status));
	}
}