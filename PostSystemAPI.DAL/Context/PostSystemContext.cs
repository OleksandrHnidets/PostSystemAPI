using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Context
{
    public class PostSystemContext: DbContext
    {
        public PostSystemContext(DbContextOptions<PostSystemContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<PostOffice> PostOffices { get; set; }

        public DbSet<Receiver> Receivers { get; set; }

        public DbSet<Sender> Senders { get; set; }
    }
}
