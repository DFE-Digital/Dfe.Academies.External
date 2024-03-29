﻿using AutoFixture;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationContributorTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string firstName = Fixture.Create<string>();
		string surName = Fixture.Create<string>();
		string email = Fixture.Create<string>();
		string? role = null;
		SchoolRoles schoolRole = SchoolRoles.ChairOfGovernors;

		var applicationComponent = new ConversionApplicationContributor(firstName, surName, email, schoolRole, role)
		{
			ContributorId = int.MaxValue
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.ContributorId, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.FirstName, Is.EqualTo(firstName));
		Assert.That(applicationComponent.LastName, Is.EqualTo(surName));
		Assert.That(applicationComponent.Role, Is.EqualTo(schoolRole));
	}
}
