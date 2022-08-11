using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolPupilNumbersSummarySectionViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string name = Fixture.Create<string>();
		string answer = Fixture.Create<string>();

		var schoolPupilNumbersSummarySectionViewModel = new SchoolPupilNumbersSummarySectionViewModel(name, answer);

		// act
		// nothing!

		// assert
		Assert.That(schoolPupilNumbersSummarySectionViewModel, Is.Not.Null);
		Assert.That(schoolPupilNumbersSummarySectionViewModel.Name, Is.EqualTo(name));
		Assert.That(schoolPupilNumbersSummarySectionViewModel.Answer, Is.EqualTo(answer));
	}
}