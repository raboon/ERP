namespace Gastro
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Collections.Generic;
    using System.Xml;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Data.Common;

    public partial class Gastro : DbContext
    {
        public const string SQL_SERVER = "Server=localhost\\SQLEXPRESS;Database=ERP;User Id=sa;Password=raboon430;";
        public const string  MYSQL_SERVER = "Server=localhost;Port=3306;Database=erp;Uid=root;Pwd=raboon430";

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<BankInfo> BankInfos { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<EmployeeTumeSheet> TimeSheets { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Product> Products { get; set; }        

        public Gastro() : base(SQL_SERVER)
        {
            System.Data.Entity.Database.SetInitializer(new Gastro.GastroInitializer());
        }
        
        public Gastro(DbConnection connection, bool contextOwnsConnection)
       : base(connection, contextOwnsConnection)
        {
            System.Data.Entity.Database.SetInitializer(new Gastro.GastroInitializer());
        }
        public Gastro(string connString) : base(connString)
        {
            System.Data.Entity.Database.SetInitializer(new Gastro.GastroInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>()
                .Where(p => p.Name.EndsWith("UTC"))
                .Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Division>()
                .Property(e => e.Description)
                .IsUnicode(false);
            
        }

        public class GastroInitializer : CreateDatabaseIfNotExists<Gastro>
        {
            protected override void Seed(Gastro context)
            {
                try
                {
                    Console.WriteLine("Seed tables...");
                    /** TODO: Seed script will go here to init */

                    Console.WriteLine("Seed tables done");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Seed tables failed");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
