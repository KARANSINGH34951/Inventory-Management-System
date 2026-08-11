using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.DTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int MinimumStockLevel { get; set; }

        public int CategoryId { get; set; }
    }
}
