
namespace PhoneLogic.Core
{
    public static class CallContextApp
    {
        public const string RecknerCallAppGuid = "{E7D2695C-96F8-4C49-858A-28F6106B2B39}";
    }

    public static class RecknerLyncTools
    {
        public static string SipFormatted(string sip)
        {
            return sip.Substring(5);
        }
    }
}


