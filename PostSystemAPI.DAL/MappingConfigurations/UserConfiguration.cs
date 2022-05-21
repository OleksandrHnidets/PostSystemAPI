using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.MappingConfigurations
{
    internal class UserConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<User>()
                    .HasMany(r => r.SendedDeliveries)
                    .WithOne(u => u.SendedUser)
                    .HasForeignKey(e => e.SendedBy)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                    .HasMany(r => r.ReceivedDeliveries)
                    .WithOne(u => u.ReceivedUser)
                    .HasForeignKey(e => e.ReceivedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Delivery>().Property(e=>e.Id).IsRequired().HasDefaultValueSql("NEWID()");
        }
    }
}
