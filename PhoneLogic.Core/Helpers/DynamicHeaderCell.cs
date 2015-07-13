using System;

namespace PhoneLogic.Core.Helpers
{

    public class DynamicHeaderCell
    {
        public String Header { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }

        public DynamicHeaderCell(String header)
        {
            RowSpan = 1;
            ColSpan = 1;
            Header = header;
        }
    }
}
