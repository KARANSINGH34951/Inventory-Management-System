using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Infrastructure.Data.Configurations
{
    public class StockTransactionConfiguration
    : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.HasKey(st => st.Id);

            builder.Property(st => st.Quantity)
                .IsRequired();

            builder.Property(st => st.PreviousQuantity)
                .IsRequired();

            builder.Property(st => st.NewQuantity)
                .IsRequired();

            builder.Property(st => st.Type)
                .IsRequired();

            builder.Property(st => st.Reason)
                .HasMaxLength(500);

            builder.HasOne(st => st.Product)
                .WithMany()
                .HasForeignKey(st => st.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(st => st.User)
                .WithMany(u => u.StockTransactions)
                .HasForeignKey(st => st.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(st => st.ProductId);

            builder.HasIndex(st => st.UserId);

            builder.HasIndex(st => st.CreatedAt);
        }
    }
}
