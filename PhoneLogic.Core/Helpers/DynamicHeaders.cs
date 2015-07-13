using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PhoneLogic.Core.Helpers
{
    public class DynamicHeaders
    {
        List<DynamicHeader> Headers;
        int HeaderRows;
        int HeaderCols;

        public DynamicHeaders(String Header)
        {
            Headers = new List<DynamicHeader>();
            String[] HeaderParts = Header.Split('|');
            foreach (String tmpHeaderPart in HeaderParts)
                Headers.Add(new DynamicHeader(tmpHeaderPart));
            HeaderCols = Headers.Count;
            HeaderRows = Headers.Max(H => H.HeaderDepth);
            ParseHeader();
        }

        public List<List<DynamicHeaderCell>> ParseHeader()
        {
            //Creating an Array with the headings
            var headerCells = new DynamicHeaderCell[HeaderRows, HeaderCols];
            for (int i = 0; i < Headers.Count; i++)
            {
                var tmpDynamicHeader = Headers[i];
                for (int j = 0; j < tmpDynamicHeader.Headers.Length; j++)
                {
                    String headerString = tmpDynamicHeader.Headers[j];
                    headerCells.SetValue(new DynamicHeaderCell(headerString), j, i);
                }
            }

            //Marge the columns
            for (int i = 0; i < HeaderRows; i++)
            {
                for (int j = 0; j < HeaderCols; j++)
                {
                    var headerCell1 = (DynamicHeaderCell)headerCells.GetValue(i, j);
                    if (headerCell1 == null)
                        continue;
                    for (int k = j + 1; k < HeaderCols; k++)
                    {
                        var headerCell2 = (DynamicHeaderCell)headerCells.GetValue(i, k);
                        if (headerCell2 == null)
                            continue;
                        if (headerCell1.Header.Equals(headerCell2.Header))
                        {
                            headerCell1.ColSpan++;
                            headerCells.SetValue(null, i, k);
                        }
                    }
                }
            }

            //Marge the Rows
            for (int j = 0; j < HeaderCols; j++)
            {
                for (int i = 0; i < HeaderRows; i++)
                {
                    var headerCell1 = (DynamicHeaderCell)headerCells.GetValue(i, j);
                    if (headerCell1 == null)
                        continue;

                    for (int k = i + 1; k < HeaderRows; k++)
                    {
                        var headerCell2 = (DynamicHeaderCell)headerCells.GetValue(k, j);
                        if (headerCell2 == null)
                        {
                            headerCell1.RowSpan++;
                            headerCells.SetValue(null, k, j);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            var dynHeaderCells = new List<List<DynamicHeaderCell>>();
            for (int i = 0; i < HeaderRows; i++)
            {
                var rowHeaderCells = new List<DynamicHeaderCell>();
                for (int j = 0; j < HeaderCols; j++)
                {
                    var headerCell = (DynamicHeaderCell)headerCells.GetValue(i, j);
                    if (headerCell == null)
                        continue;
                    rowHeaderCells.Add(headerCell);
                }
                dynHeaderCells.Add(rowHeaderCells);
            }
            return dynHeaderCells;
        }
    }
}
