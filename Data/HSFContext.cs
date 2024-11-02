using Hsf_Receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace Hsf_Receitas.Data
{
    public class HSFContext : DbContext
    {
        public HSFContext()
        {
            // Construtor
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=127.0.0.1;port=33600;database=hsf_db;uid=hsf.connection;password=Root@123456;SslMode=Preferred;ConvertZeroDateTime=true;pooling=no"
            );
        }

        //server=mysql5048.site4now.net;port=3306;database=db_a9b2e5_hsfdb;uid=a9b2e5_hsfdb;password=Ann@1170615;SslMode=Preferred;ConvertZeroDateTime=true;pooling=no

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicacao>().ToTable("Medicamentos");
            modelBuilder.Entity<Receituario>().ToTable("Receitas");
            modelBuilder.Entity<AtestadoMedico>().ToTable("ATM");
            modelBuilder.Entity<AtestadoComparecimento>().ToTable("ATC");
        }

        public DbSet<Medicacao> Medicamentos { get; set; }
        public DbSet<Receituario> Receitas { get; set; }
        public DbSet<AtestadoMedico> AtestadosMedico { get; set; }
        public DbSet<AtestadoComparecimento> AtestadosComparecimento { get; set; }
    }
}
