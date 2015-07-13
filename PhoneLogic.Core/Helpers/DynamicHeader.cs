using System;

namespace PhoneLogic.Core.Helpers
{

    public class DynamicHeader
    {
        public int HeaderDepth { get; set; }
        public String[] Headers { get; set; }

        public DynamicHeader(String header)
        {
            Headers = header.Split('|');
            HeaderDepth = Headers.Length;
        }
    }
}