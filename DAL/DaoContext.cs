using Authentic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentic.DAL
{
    public class DaoContext : IdentityDbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set;}

        public DaoContext()
        {
        } 
        public DaoContext(DbContextOptions<DaoContext> options) : base(options)
        {
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=ECOMINA-DELL\\SQLEXPRESS;Initial Catalog=NexttPromo;User ID=sa;Password=nexttsol");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(pk => pk.Id);
            // modelBuilder.Entity<Usuario>().Property(p => p.Id).HasColumnName("ID").HasColumnType("uniqueidentifier");
        }

    }
}