using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfViewer2021
{
    public static class StringExt
    {
        public static bool Contains
        (
            this string self,
            string value,
            StringComparison comparisonType
        )
        {
            return self.IndexOf(value, comparisonType) != -1;
        }
    }
}
