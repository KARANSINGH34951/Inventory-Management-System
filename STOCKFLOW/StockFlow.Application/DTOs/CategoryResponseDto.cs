using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Application.DTOs
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int ProductCount { get; set; }
    }
}
