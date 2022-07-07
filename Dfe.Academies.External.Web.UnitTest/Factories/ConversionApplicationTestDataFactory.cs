using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using System.Collections.Generic;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class ConversionApplicationTestDataFactory
{
    private static readonly Fixture Fixture = new();
    private static readonly UniqueRecordIdentifierGenerator UniqueRecordIdentifierGenerator = new();

    public static ConversionApplication BuildNewConversionApplicationNoRoles()
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

    public static ConversionApplication BuildNewConversionApplicationWithOtherRole()
    {
        return new ConversionApplication
        {
            TrustName = Fixture.Create<string>(),
            UserEmail = Fixture.Create<string>(),
            Id = UniqueRecordIdentifierGenerator.GenerateId(),
            ApplicationType = ApplicationTypes.FormNewMat,
            Application = Fixture.Create<string>(),
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(),
            SchoolRole = SchoolRoles.Other,
            OtherRoleNotListed = Fixture.Create<string>(),
        };
    }

    public static ConversionApplication BuildNewConversionApplicationWithChairRole()
    {
        return new ConversionApplication
        {
            TrustName = Fixture.Create<string>(),
            UserEmail = Fixture.Create<string>(),
            Id = UniqueRecordIdentifierGenerator.GenerateId(),
            ApplicationType = ApplicationTypes.FormNewMat,
            Application = Fixture.Create<string>(),
            SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>(),
            SchoolRole = SchoolRoles.Chair
        };
    }
}