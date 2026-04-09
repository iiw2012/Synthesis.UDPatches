using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosedXML.Excel
{
    public static class XLCellExtensions
    {
        public static XLCellValue MergedValue(this IXLCell cell)
        {
            if (cell.IsMerged())
            {
                return cell.MergedRange().FirstCellUsed().Value;
            }
            else
            {
                return cell.Value;
            }
        }
    }
}
