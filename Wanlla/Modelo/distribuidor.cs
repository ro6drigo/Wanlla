namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribuidor")]
    public partial class distribuidor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public distribuidor()
        {
            producto = new HashSet<producto>();
        }

        [Key]
        public int id_distribuidor { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_distribuidor { get; set; }

        [Required]
        [StringLength(300)]
        public string email_distribuidor { get; set; }

        [Required]
        [StringLength(15)]
        public string tel_distribuidor { get; set; }

        [Required]
        [StringLength(255)]
        public string pass_distribuidor { get; set; }

        [Required]
        [StringLength(50)]
        public string estado_distribuidor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto> producto { get; set; }
    }
}
