using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.MappingConfigurations
{
    internal class TransactionHistoryConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<Delivery>()
                .HasOne<TransactionHistory>(r => r.TransactionHistory)
                .WithOne(u => u.Delivery)
                .HasForeignKey<TransactionHistory>(u => u.DeliveryId);

            builder.Entity<TransactionHistory>().Property(e => e.Id).IsRequired().HasDefaultValueSql("NEWID()");
        }
    }
}
