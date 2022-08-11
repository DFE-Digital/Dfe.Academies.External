using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolPupilNumbersSummaryHeadingViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string title = Fixture.Create<string>();
		string uri = Fixture.Create<string>();

		var numbersSummaryHeadingViewModel = new SchoolPupilNumbersSummaryHeadingViewModel(title, uri);

		// act
		// nothing!

		// assert
		Assert.That(numbersSummaryHeadingViewModel, Is.Not.Null);
		Assert.That(numbersSummaryHeadingViewModel.Title, Is.EqualTo(title));
		Assert.That(numbersSummaryHeadingViewModel.URI, Is.EqualTo(uri));
		Assert.That(numbersSummaryHeadingViewModel.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
	}
}