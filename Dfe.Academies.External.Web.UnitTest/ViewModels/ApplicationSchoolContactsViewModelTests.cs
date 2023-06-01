using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationSchoolContactsViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		int urn = Fixture.Create<int>();
		string contactHeadName = Fixture.Create<string>();
		string contactHeadEmail = Fixture.Create<string>();
		string contactHeadTel = Fixture.Create<string>();
		string contactChairName = Fixture.Create<string>();
		string contactChairEmail = Fixture.Create<string>();
		string contactChairTel = Fixture.Create<string>();
		MainConversionContact contactRole = Fixture.Create<MainConversionContact>();

		var schoolDetailsViewModel = new ApplicationSchoolContactsViewModel(applicationId, urn)
		{
			ContactHeadName = contactHeadName,
			ContactHeadEmail = contactHeadEmail,
			ContactChairName = contactChairName,
			ContactChairEmail = contactChairEmail,
			ContactRole = contactRole
		};

		// act
		// nothing!

		// assert
		Assert.That(schoolDetailsViewModel, Is.Not.Null);
		Assert.That(schoolDetailsViewModel.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(schoolDetailsViewModel.Urn, Is.EqualTo(urn));
		Assert.That(schoolDetailsViewModel.ContactHeadName, Is.EqualTo(contactHeadName));
		Assert.That(schoolDetailsViewModel.ContactHeadEmail, Is.EqualTo(contactHeadEmail));
		Assert.That(schoolDetailsViewModel.ContactChairName, Is.EqualTo(contactChairName));
		Assert.That(schoolDetailsViewModel.ContactChairEmail, Is.EqualTo(contactChairEmail));
		Assert.That(schoolDetailsViewModel.ContactRole, Is.EqualTo(contactRole));
	}
}