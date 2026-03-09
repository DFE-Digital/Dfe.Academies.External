using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationSchoolContactsViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor_WithApplicationIdAndUrn_SetsProperties()
	{
		int applicationId = Fixture.Create<int>();
		int urn = Fixture.Create<int>();
		string contactHeadName = Fixture.Create<string>();
		string contactHeadEmail = Fixture.Create<string>();
		string contactChairName = Fixture.Create<string>();
		string contactChairEmail = Fixture.Create<string>();
		MainConversionContact contactRole = MainConversionContact.HeadTeacher;

		var model = new ApplicationSchoolContactsViewModel(applicationId, urn)
		{
			ContactHeadName = contactHeadName,
			ContactHeadEmail = contactHeadEmail,
			ContactChairName = contactChairName,
			ContactChairEmail = contactChairEmail,
			ContactRole = contactRole
		};

		Assert.That(model, Is.Not.Null);
		Assert.That(model.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(model.Urn, Is.EqualTo(urn));
		Assert.That(model.ContactHeadName, Is.EqualTo(contactHeadName));
		Assert.That(model.ContactHeadEmail, Is.EqualTo(contactHeadEmail));
		Assert.That(model.ContactChairName, Is.EqualTo(contactChairName));
		Assert.That(model.ContactChairEmail, Is.EqualTo(contactChairEmail));
		Assert.That(model.ContactRole, Is.EqualTo(contactRole));
	}

	[Test]
	public void ParameterlessConstructor_CreatesInstance_WithDefaultValues()
	{
		var model = new ApplicationSchoolContactsViewModel();

		Assert.That(model, Is.Not.Null);
		Assert.That(model.ApplicationId, Is.EqualTo(0));
		Assert.That(model.Urn, Is.EqualTo(0));
		Assert.That(model.ContactHeadName, Is.Null);
		Assert.That(model.ContactHeadEmail, Is.Null);
		Assert.That(model.ContactChairName, Is.Null);
		Assert.That(model.ContactChairEmail, Is.Null);
		Assert.That(model.ContactRole, Is.EqualTo(default(MainConversionContact)));
		Assert.That(model.MainContactOtherName, Is.Null);
		Assert.That(model.MainContactOtherEmail, Is.Null);
		Assert.That(model.MainContactOtherRole, Is.Null);
		Assert.That(model.ApproverContactName, Is.Null);
		Assert.That(model.ApproverContactEmail, Is.Null);
	}

	[Test]
	public void OptionalProperties_CanBeSet_WhenContactRoleIsOther()
	{
		var model = new ApplicationSchoolContactsViewModel(123, 456)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other Contact",
			MainContactOtherEmail = "other@school.com",
			MainContactOtherRole = "Clerk",
			ApproverContactName = "Approver",
			ApproverContactEmail = "approver@school.com"
		};

		Assert.That(model.MainContactOtherName, Is.EqualTo("Other Contact"));
		Assert.That(model.MainContactOtherEmail, Is.EqualTo("other@school.com"));
		Assert.That(model.MainContactOtherRole, Is.EqualTo("Clerk"));
		Assert.That(model.ApproverContactName, Is.EqualTo("Approver"));
		Assert.That(model.ApproverContactEmail, Is.EqualTo("approver@school.com"));
	}

	[Test]
	public void Validation_WhenAllRequiredFieldsValid_ReturnsNoErrors()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head Teacher",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair Person",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Is.Empty);
	}

	[Test]
	public void Validation_WhenContactHeadNameMissing_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = null!,
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactHeadName)) &&
			r.ErrorMessage?.Contains("headteacher") == true));
	}

	[Test]
	public void Validation_WhenContactHeadEmailMissing_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = null!,
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactHeadEmail))));
	}

	[Test]
	public void Validation_WhenContactHeadEmailInvalidFormat_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "not-an-email",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactHeadEmail)) &&
			r.ErrorMessage?.Contains("Headteacher") == true));
	}

	[Test]
	public void Validation_WhenContactChairNameMissing_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = null!,
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactChairName))));
	}

	[Test]
	public void Validation_WhenContactChairEmailInvalidFormat_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "invalid",
			ContactRole = MainConversionContact.HeadTeacher
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactChairEmail)) &&
			r.ErrorMessage?.Contains("Chair") == true));
	}

	[Test]
	public void Validation_WhenContactRoleNotSet_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = default
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ContactRole)) &&
			r.ErrorMessage?.Contains("main contact") == true));
	}

	[Test]
	public void Validation_WhenMainContactOtherEmailInvalidFormat_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other",
			MainContactOtherEmail = "not-valid-email"
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.MainContactOtherEmail)) &&
			r.ErrorMessage?.Contains("Other contact") == true));
	}

	[Test]
	public void Validation_WhenApproverContactEmailInvalidFormat_ReturnsError()
	{
		var model = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher,
			ApproverContactEmail = "bad-email"
		};

		var results = ValidateModel(model);

		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.MemberNames.Contains(nameof(ApplicationSchoolContactsViewModel.ApproverContactEmail)) &&
			r.ErrorMessage?.Contains("Approver") == true));
	}

	private static List<ValidationResult> ValidateModel(object model)
	{
		var results = new List<ValidationResult>();
		var context = new ValidationContext(model, null, null);
		Validator.TryValidateObject(model, context, results, true);
		return results;
	}
}
