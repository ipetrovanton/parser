namespace DataBaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dateOfPurchase")]
    public partial class dateOfPurchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dateOfPurchase()
        {
            AllData = new HashSet<AllData>();
        }

        public int id { get; set; }

        [Column("DateOfPurchase", TypeName = "date")]
        public DateTime? DateOfPurchase1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllData> AllData { get; set; }
    }
}
