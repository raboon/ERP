namespace Gastro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankInfo")]
    public partial class BankInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankInfo()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(100)]
        [Index("bankName", 1)]
        public string Name { get; set; }

        [StringLength(50)]
        [Index("bic", 2)]
        public string Bic { get; set; }

        [StringLength(18)]
        [Index("iban",3)]
        public string IBAN { get; set; }

        [StringLength(50)]        
        public string AccountNo { get; set; }

        [StringLength(50)]        
        public string SwiftCode { get; set; }        

        public virtual Employee employee { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SessionBand> sessionbands { get; set; }

        //public virtual Technology technology { get; set; }
    }
}

