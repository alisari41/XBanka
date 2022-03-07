using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class BankaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=tcp:alisqlserver41.database.windows.net,1433;Initial Catalog=Banka;Persist Security Info=False;User ID=alisari41;Password=ali1234*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Kanal> Kanallar{ get; set; }
        public DbSet<Adres> Adresler{ get; set; }
        public DbSet<Sendika> Sendikalar{ get; set; }


        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
