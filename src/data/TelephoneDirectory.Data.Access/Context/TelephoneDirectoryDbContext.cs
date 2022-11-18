using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Data.Entities;

namespace TelephoneDirectory.Data.Access.Context
{
    public class TelephoneDirectoryDbContext : DbContext
    {
        public TelephoneDirectoryDbContext(DbContextOptions<TelephoneDirectoryDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonContactEntityConfiguration());
        }
    }

}
