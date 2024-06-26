﻿using Dfe.Academies.External.Web.Routing;
using NUnit.Framework;
using NUnit.Framework.Legacy;


namespace DfE.Academies.External.Web.UnitTest.Routing
{
	internal sealed class HyphenateRouteParameterTransformerTest
	{
		[Test]
		public void TransformOutbound___NullParameter___ReturnsNull()
		{
			var target = new HyphenateRouteParameterTransformer();

			object? parameter = null;

			var result = target.TransformOutbound(parameter);

			ClassicAssert.IsNull(result);
		}

		[TestCase(1)]
		[TestCase(1.0)]
		public void TransformOutbound___NonStringParameter___ReturnsHyphenatedString(object? parameter)
		{
			var target = new HyphenateRouteParameterTransformer();

			var result = target.TransformOutbound(parameter);

			ClassicAssert.IsNull(result);
		}

		[Test]
		public void TransformOutbound___NullStringParameter___ReturnsNull()
		{
			var target = new HyphenateRouteParameterTransformer();

			string? parameter = null;

			var result = target.TransformOutbound(parameter);

			ClassicAssert.IsNull(result);
		}

		[TestCase("", "")]
		[TestCase("1", "1")]
		[TestCase("HyphenateThisUrl", "hyphenate-this-url")]
		[TestCase("hyphenate-this-url", "hyphenate-this-url")]
		[TestCase("lowercaseurl", "lowercaseurl")]
		public void TransformOutbound___CamelCaseStringParameter___ReturnsHyphenatedString(object? parameter, string? expected)
		{
			var target = new HyphenateRouteParameterTransformer();

			var result = target.TransformOutbound(parameter);

			ClassicAssert.AreEqual(expected, result);
		}
	}
}
