using StockFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; }
            = new List<StockTransaction>();

        public ICollection<AuditLog> AuditLogs { get; set; }
            = new List<AuditLog>();
    }
}
