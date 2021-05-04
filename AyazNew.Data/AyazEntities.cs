using AyazNew.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Data
{
    public class AyazEntities : DbContext
    {
        public AyazEntities()
            : base("name=AyazConnectionString")
        {
        }

        public DbSet<Products> Bike { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Categories> Categories { get; set; }

    }
}
