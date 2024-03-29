﻿using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ValidationErrorMessagesViewModelTests
{
	[Test]
	public void ValidationErrorMessagesViewModel___PropertyCheck___Success()
	{
		// arrange
		var validationErrorMessagesViewModel = new ValidationErrorMessagesViewModel();

		validationErrorMessagesViewModel.ValidationErrorMessages.Add("OtherRoleNotEntered", new[] { "You must give your role" });

		// assert
		Assert.That(validationErrorMessagesViewModel, Is.Not.Null);
		Assert.That(validationErrorMessagesViewModel.ValidationErrorMessages.Count, Is.EqualTo(1));
	}
}