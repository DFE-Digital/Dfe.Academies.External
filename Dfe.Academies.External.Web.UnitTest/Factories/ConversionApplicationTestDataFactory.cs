using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using System.Collections.Generic;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class ConversionApplicationTestDataFactory
{
    private static readonly Fixture Fixture = new();
    private static readonly UniqueRecordIdentifierGenerator UniqueRecordIdentifierGenerator = new();

    public static ConversionApplication BuildNewConversionApplication()
    {
        return new ConversionApplication
        {
            TrustName = Fixture.Create<string>(),
            UserEmail = Fixture.Create<string>(),
            Id = UniqueRecordIdentifierGenerator.GenerateId(),
            ApplicationType = ApplicationTypes.FormNewMat,
            Application = Fixture.Create<string>(),
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>()
        };
    }
}