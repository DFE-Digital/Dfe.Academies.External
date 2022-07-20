using Dfe.Academies.External.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolControllerTests
{
	// TODO MR:- test Overview(int appId, int applyingSchoolId)
	// returns IActionResult = html / razor page

	// TODO MR:- test Search(string searchQuery)
	// searchQuery = schoolname = 'wise' OR URN = 587
	// returns IEnumerable<string>

	// TODO MR:- ReturnSchoolDetailsPartialViewPopulated(string selectedSchool)
	// selectedSchool = format from autocomplete = '$"{schoolName} ({urn})"'
	// returns IActionResult = html / razor page
}