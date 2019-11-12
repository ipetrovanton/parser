namespace DBFirstLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DateOfEnd")]
    public partial class DateOfEnd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DateOfEnd()
        {
            AllData = new HashSet<AllData>();
        }

        public int id { get; set; }

        [Column("dateOfEnd", TypeName = "date")]
        public DateTime? dateOfEnd1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllData> AllData { get; set; }
    }
}
