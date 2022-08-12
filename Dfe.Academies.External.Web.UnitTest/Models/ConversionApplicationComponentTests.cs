using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

[Parallelizable(ParallelScope.All)]
internal sealed class ConversionApplicationComponentTests
{
    private static readonly Fixture Fixture = new();

    [Test]
    public void Constructor___PropertiesSet()
    {
        // arrange
        string name = Fixture.Create<string>();
        var status = Status.NotStarted;

        var applicationComponent = new ConversionApplicationComponent(name)
        {
            Id = int.MaxValue,
            Status = status
        };

        // act
        // nothing!

        // assert
        Assert.That(applicationComponent, Is.Not.Null);
        Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
        Assert.That(applicationComponent.Status, Is.EqualTo(status));
        Assert.That(applicationComponent.Name, Is.EqualTo(name));
    }
}