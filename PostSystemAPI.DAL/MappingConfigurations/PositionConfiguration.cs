using System;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Enums;
using PostSystemAPI.DAL.Models;

namespace PostSystemAPI.DAL.MappingConfigurations;

internal class PositionConfiguration
{
    public static void Configure(ModelBuilder builder)
    {
        builder.Entity<Position>()
            .Property(x => x.CurrentDriverStatus)
            .HasConversion(x => x.ToString(), x => Enum.Parse<CurrentDriverStatus>(x));

        
        builder.Entity<Position>().Property(e=>e.Id).IsRequired().HasDefaultValueSql("NEWID()");
    }
}