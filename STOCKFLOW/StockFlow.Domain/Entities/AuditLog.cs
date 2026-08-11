using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string EntityName { get; set; } = string.Empty;

        public int? EntityId { get; set; }

        public string? Details { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
