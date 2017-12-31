namespace Gastro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Plan")]
    public partial class Plan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plan()
        {
            //sessionbands = new HashSet<SessionBand>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index]
        [StringLength(10)]
        public string WorkDate { get; set; }        
        public int? BranchId { get; set; }        
        public int? DivId { get; set; }
        public int AssignerID { get; set; }
        public int EmoloyeeID { get; set; }
        public DateTime ShiftStartUTC { get; set; }
        public DateTime ShiftEndUTC { get; set; }
        public virtual Employee AssignedBy { get; set; }
        public virtual Employee AssignedTo { get; set; }
        public virtual Branch Branch{ get; set; }
        public virtual Division Division { get; set; }
        public virtual Employee AssignedEmployee { get; set; }
    }
}