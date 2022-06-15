using System.Text.RegularExpressions;

namespace Dfe.Academies.External.Web.Routing
{
    public class HyphenateRouteParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
            {
                return null;
            }

            string? stringValue = value.ToString();

            if (stringValue == null)
            {
                return null;
            }

            return Regex.Replace(stringValue, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
