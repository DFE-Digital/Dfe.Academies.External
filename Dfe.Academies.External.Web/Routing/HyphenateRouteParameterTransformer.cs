using System.Text.RegularExpressions;

namespace Dfe.Academies.External.Web.Routing
{
    public class HyphenateRouteParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value is not string stringValue)
            {
                return null;
            }

            return Regex.Replace(stringValue, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
