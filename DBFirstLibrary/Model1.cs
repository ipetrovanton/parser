namespace DBFirstLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ConnectionDB")
        {
        }

        public virtual DbSet<AllData> AllData { get; set; }
        public virtual DbSet<customerName> customerName { get; set; }
        public virtual DbSet<DateOfEnd> DateOfEnd { get; set; }
        public virtual DbSet<dateOfPurchase> dateOfPurchase { get; set; }
        public virtual DbSet<NumbersOfContracts> NumbersOfContracts { get; set; }
        public virtual DbSet<PurchaisingSection> PurchaisingSection { get; set; }
        public virtual DbSet<PurchaisingType> PurchaisingType { get; set; }
        public virtual DbSet<PurchaseName> PurchaseName { get; set; }
        public virtual DbSet<StartPrice> StartPrice { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<customerName>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.customerName1)
                .HasForeignKey(e => e.CustomerName);

            modelBuilder.Entity<DateOfEnd>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.DateOfEnd1)
                .HasForeignKey(e => e.DateOfEnd);

            modelBuilder.Entity<dateOfPurchase>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.dateOfPurchase)
                .HasForeignKey(e => e.DateOfPurchaise);

            modelBuilder.Entity<NumbersOfContracts>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.NumbersOfContracts)
                .HasForeignKey(e => e.NumberOfContract);

            modelBuilder.Entity<PurchaisingSection>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.PurchaisingSection1)
                .HasForeignKey(e => e.PurchaisingSection);

            modelBuilder.Entity<PurchaisingType>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.PurchaisingType1)
                .HasForeignKey(e => e.PurchaisingType);

            modelBuilder.Entity<PurchaseName>()
                .Property(e => e.PurchaseName1)
                .IsFixedLength();

            modelBuilder.Entity<PurchaseName>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.PurchaseName)
                .HasForeignKey(e => e.PurchaiseName);

            modelBuilder.Entity<StartPrice>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StartPrice>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.StartPrice1)
                .HasForeignKey(e => e.StartPrice);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.AllData)
                .WithOptional(e => e.Status1)
                .HasForeignKey(e => e.Status);
        }
    }
}
