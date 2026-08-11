using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinimumStockLevel { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Category Category { get; set; } = null!;
    }

}
