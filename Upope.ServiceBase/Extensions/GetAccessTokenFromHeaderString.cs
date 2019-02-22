
namespace Upope.ServiceBase.Extensions
{
    public static class StringExtensions
    {
        public static string GetAccessTokenFromHeaderString(this string headerString)
        {
            return headerString.Replace("Bearer ", "");
        }
    }
}
