using System;
using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationAuditEntryTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string createdBy = Fixture.Create<string>();
		string typeOfChange = Fixture.Create<string>();
		string entityChanged = Fixture.Create<string>();
		string propertyChanged = Fixture.Create<string>();
		DateTime when = Fixture.Create<DateTime>();

		var conversionApplicationAuditEntry = new ConversionApplicationAuditEntry(createdBy, typeOfChange, entityChanged, propertyChanged)
		{
			Id = int.MaxValue,
			DateCreated = when
		};

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.Id, Is.EqualTo(int.MaxValue));
		Assert.That(conversionApplicationAuditEntry.DateCreated, Is.EqualTo(when));
		Assert.That(conversionApplicationAuditEntry.CreatedBy, Is.EqualTo(createdBy));
		Assert.That(conversionApplicationAuditEntry.TypeOfChange, Is.EqualTo(typeOfChange));
		Assert.That(conversionApplicationAuditEntry.EntityChanged, Is.EqualTo(entityChanged));
		Assert.That(conversionApplicationAuditEntry.PropertyChanged, Is.EqualTo(propertyChanged));
	}
}