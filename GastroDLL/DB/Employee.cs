namespace Gastro
{    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            //sessionbands = new HashSet<SessionBand>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(PropertyLengths.Name)]
        public string Name { get; set; }
        [Required]
        public DateTime DBO { get; set; }
        [Required]
        [MaxLength(PropertyLengths.Password)]
        public byte[] Password { get; set; }

        [Required]
        [StringLength(50)]        
        public string LastName { get; set; }

        [Required]
        [MaxLength(PropertyLengths.UserEmailMax)]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [System.ComponentModel.DefaultValue(0)]
        public int Dbrole { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IsDeleted { get; set; }        
        public int? BranchID { get; set; }
        public int? DivisionID { get; set; }
    }
}

