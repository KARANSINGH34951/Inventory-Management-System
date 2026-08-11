using StockFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public int PreviousQuantity { get; set; }

        public int NewQuantity { get; set; }

        public StockTransactionType Type { get; set; }

        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
