namespace DataBaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AllData")]
    public partial class AllData
    {
        public int id { get; set; }

        public int? NumberOfContract { get; set; }

        public int? CustomerName { get; set; }

        public int? DateOfPurchaise { get; set; }

        public int? DateOfEnd { get; set; }

        public int? PurchaiseName { get; set; }

        public int? StartPrice { get; set; }

        public int? Status { get; set; }

        public int? PurchaisingType { get; set; }

        public int? PurchaisingSection { get; set; }

        public virtual customerName customerName1 { get; set; }

        public virtual DateOfEnd DateOfEnd1 { get; set; }

        public virtual dateOfPurchase dateOfPurchase { get; set; }

        public virtual NumbersOfContracts NumbersOfContracts { get; set; }

        public virtual PurchaisingSection PurchaisingSection1 { get; set; }

        public virtual PurchaisingType PurchaisingType1 { get; set; }

        public virtual PurchaseName PurchaseName { get; set; }

        public virtual StartPrice StartPrice1 { get; set; }

        public virtual Status Status1 { get; set; }
    }
}
