using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationSchoolContactsTests
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
		string? mainContactOtherName = Fixture.Create<string>();
		string? mainContactOtherEmail = Fixture.Create<string>();
		string? mainContactOtherTelephone = Fixture.Create<string>();
		string? approverContactName = Fixture.Create<string>();
		string? approverContactEmail = Fixture.Create<string>();

		var schoolDetailsViewModel = new ApplicationSchoolContacts(applicationId, urn,
			contactHeadName,
			contactHeadEmail,
			contactHeadTel,
			contactChairName,
			contactChairEmail,
			contactChairTel,
			contactRole,
			mainContactOtherName,
			mainContactOtherEmail,
			mainContactOtherTelephone,
			approverContactName,
			approverContactEmail);

		// act
		// nothing!

		// assert
		Assert.That(schoolDetailsViewModel, Is.Not.Null);
		Assert.That(schoolDetailsViewModel.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(schoolDetailsViewModel.Urn, Is.EqualTo(urn));
		Assert.That(schoolDetailsViewModel.ContactHeadName, Is.EqualTo(contactHeadName));
		Assert.That(schoolDetailsViewModel.ContactHeadEmail, Is.EqualTo(contactHeadEmail));
		Assert.That(schoolDetailsViewModel.ContactHeadTel, Is.EqualTo(contactHeadTel));
		Assert.That(schoolDetailsViewModel.ContactChairName, Is.EqualTo(contactChairName));
		Assert.That(schoolDetailsViewModel.ContactChairEmail, Is.EqualTo(contactChairEmail));
		Assert.That(schoolDetailsViewModel.ContactChairTel, Is.EqualTo(contactChairTel));
		Assert.That(schoolDetailsViewModel.ContactRole, Is.EqualTo(contactRole));
	}
}