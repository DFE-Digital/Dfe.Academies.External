﻿using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.AcademiesAPIResponseModels.Schools;

internal sealed class SchoolSearchTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolSearch___PropertyCheck___Success()
	{
		// arrange
		string schoolName = Fixture.Create<string>();
		string urn = Fixture.Create<string>();
		string ukprn = Fixture.Create<string>();

		var schoolSearch = new SchoolSearch(schoolName, urn, ukprn);

		// act
		// nothing!

		// assert
		Assert.That(schoolSearch, Is.Not.Null);
		Assert.That(schoolSearch.Name, Is.EqualTo(schoolName));
		Assert.That(schoolSearch.Urn, Is.EqualTo(urn));
		Assert.That(schoolSearch.Ukprn, Is.EqualTo(ukprn));
	}
}