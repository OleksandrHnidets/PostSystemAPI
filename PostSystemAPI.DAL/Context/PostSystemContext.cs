using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.MappingConfigurations;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Context
{
    public class PostSystemContext: IdentityDbContext<User>
    {
        public PostSystemContext(DbContextOptions<PostSystemContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<PostOffice> PostOffices { get; set; }
        public DbSet<TransactionHistory> TransactionsHistory { get; set; }
        public DbSet<Position> Positions { get; set; }

        //public DbSet<Receiver> Receivers { get; set; }

        //public DbSet<Sender> Senders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

            UserConfiguration.Configure(builder);
            PostOfficeConfiguration.Configure(builder);
            PositionConfiguration.Configure(builder);
        }
    }
}
