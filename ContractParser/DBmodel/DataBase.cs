namespace ContractParser.DBmodel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataBase : DbContext
    {
       
        public DataBase()
            : base("name=DataBase")
        {

        }

        public DbSet<ContractNumber> ContractNumbers { get; set; }
        public DbSet<CustomerName> CustomerNames { get; set; }
        public DbSet<DateOfEnd> DateOfEnds { get; set; }
        public DbSet<DateOFPurchase> DateOFPurchases { get; set; }
        public DbSet<PurchaseName> PurchaseNames { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<Section> Sections  { get; set; }
        public DbSet<StartPrice> StartPrices { get; set; }
        public DbSet<Status> Statuses { get; set; }


    }

}