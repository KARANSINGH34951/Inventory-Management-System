using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Enums
{
    public enum StockTransactionType
    {
        StockIn = 1,
        StockOut = 2,
        Adjustment = 3,
        Return = 4
    }
}
