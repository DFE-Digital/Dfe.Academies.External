using System;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class ApplicationFactory
{
	public static ConversionApplication Create(Guid entityId)
	{
		return new ConversionApplication
		{
			ApplicationId = 12345,
			ApplicationReference = "A2B_12345",
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			UserEmail = "test@example.com",
			EntityId = entityId
		};
	}

	public static ConversionApplication Create(int applicationId)
	{
		return new ConversionApplication
		{
			ApplicationId = applicationId,
			ApplicationReference = $"A2B_{applicationId}",
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			UserEmail = "test@example.com",
			EntityId = Guid.NewGuid()
		};
	}
}