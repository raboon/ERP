namespace Gastro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeTumeSheet")]
    public partial class EmployeeTumeSheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeTumeSheet()
        {
            //sessionbands = new HashSet<SessionBand>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index]
        [StringLength(10)]
        public string Day { get; set; }

        [Index]
        public int? BranchId { get; set; }
        [Index]
        public int EmployeeID { get; set; }

        [Index]
        public int? DivId { get; set; }
        public int? AprovedBy { get; set; }
        public DateTime ShiftStartUTC { get; set; }
        public DateTime ShiftEndUTC { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IsApproved { get; set; }
        public virtual Employee Approver { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Division Division { get; set; }        
        public virtual Employee AssignedEmployee { get; set; }
    }
}