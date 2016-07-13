namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("establecimiento")]
    public partial class establecimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public establecimiento()
        {
            receta_establecimiento = new HashSet<receta_establecimiento>();
        }

        [Key]
        public int id_establecimiento { get; set; }

        [StringLength(100)]
        public string nom_establecimiento { get; set; }

        [StringLength(30)]
        public string lat_establecimiento { get; set; }

        [StringLength(30)]
        public string lon_establecimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_establecimiento> receta_establecimiento { get; set; }

        public List<establecimiento> Listar()
        {
            var establecimientos = new List<establecimiento>();

            try
            {
                using (var db = new db_wanlla())
                {
                    establecimientos = db.establecimiento
                                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return establecimientos;
        }
    }
}
