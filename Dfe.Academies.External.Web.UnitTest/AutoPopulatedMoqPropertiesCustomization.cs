using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;

namespace Dfe.Academies.External.Web.UnitTest;

public class AutoPopulatedMoqPropertiesCustomization : ICustomization
{
	public void Customize(IFixture fixture)
	{
		fixture.Customizations.Add(
			new PropertiesPostprocessor(
				new MockPostprocessor(
					new MethodInvoker(
						new MockConstructorQuery()))));
		fixture.ResidueCollectors.Add(new MockRelay());
	}
}
