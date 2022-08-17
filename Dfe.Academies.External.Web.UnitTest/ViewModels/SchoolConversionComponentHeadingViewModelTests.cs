using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolConversionComponentHeadingViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string title = Fixture.Create<string>();
		string uri = Fixture.Create<string>();

		var schoolConversionComponentHeadingViewModel = new SchoolConversionComponentHeadingViewModel(title, uri);

		// act
		// nothing!

		// assert
		Assert.That(schoolConversionComponentHeadingViewModel, Is.Not.Null);
		Assert.That(schoolConversionComponentHeadingViewModel.Title, Is.EqualTo(title));
		Assert.That(schoolConversionComponentHeadingViewModel.URI, Is.EqualTo(uri));
		Assert.That(schoolConversionComponentHeadingViewModel.Status, Is.EqualTo(SchoolConversionComponentStatus.NotStarted));
	}
}