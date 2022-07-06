using AutoFixture;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationContributorTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void ConversionApplicationContributor___PropertyCheck___Success()
    {
        // arrange
        string name = Fixture.Create<string>();
        string role = Fixture.Create<string>();

        var applicationComponent = new ConversionApplicationContributor(name, role)
        {
            Id = int.MaxValue
        };

        // act
        // nothing!

        // assert
        Assert.That(applicationComponent, Is.Not.Null);
        Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
        Assert.That(applicationComponent.Name, Is.EqualTo(name));
        Assert.That(applicationComponent.Role, Is.EqualTo(role));
    }
}