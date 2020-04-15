using FirstAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Data
{
    public class FirstAPIContext : DbContext
    {

        public FirstAPIContext()
        {

        }

        public FirstAPIContext(DbContextOptions<FirstAPIContext> opitions) : base(opitions)
        {

        }
        public DbSet<Person> Persons { get; set; }

    }
}
