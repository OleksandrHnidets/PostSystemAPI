using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.MappingConfigurations
{
    internal class PostOfficeConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<PostOffice>()
                    .HasMany(r => r.SentDeliveries)
                    .WithOne(u => u.StartPostOffice)
                    .HasForeignKey(e => e.StartPostOfficeId)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<PostOffice>()
                .HasMany(r => r.ReceivedDeliveries)
                .WithOne(u => u.DestinationPostOffice)
                .HasForeignKey(e => e.DestinationPostOfficeId);
            builder.Entity<City>()
                    .HasMany(r => r.PostOffices)
                    .WithOne(u => u.City)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PostOffice>().Property(e => e.Id).IsRequired().HasDefaultValueSql("NEWID()");
            builder.Entity<City>().Property(e => e.Id).IsRequired().HasDefaultValueSql("NEWID()");
        }
    }
}
