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
                    .WithOne(u => u.SentUser)
                    .HasForeignKey(e => e.SentUserId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                    .HasMany(r => r.ReceivedDeliveries)
                    .WithOne(u => u.ReceivedUser)
                    .HasForeignKey(e => e.ReceivedUserId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(r => r.AssignedDeliveries)
                .WithOne(u => u.AssignedDriver)
                .HasForeignKey(e => e.AssignedDriverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(r => r.Positions)
                .WithOne(u => u.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Delivery>().Property(e=>e.Id).IsRequired().HasDefaultValueSql("NEWID()");
        }
    }
}
