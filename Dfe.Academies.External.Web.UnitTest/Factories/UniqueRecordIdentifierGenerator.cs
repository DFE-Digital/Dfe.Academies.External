using IdGen;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal sealed class UniqueRecordIdentifierGenerator
{
	private readonly IdGenerator _generator;

	public UniqueRecordIdentifierGenerator()
	{
		_generator = new IdGenerator(0);
	}

	public long GenerateId()
	{
		return _generator.CreateId();
	}
}

