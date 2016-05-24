namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usuario")]
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            dieta = new HashSet<dieta>();
            pedido = new HashSet<pedido>();
            receta_comentario = new HashSet<receta_comentario>();
            receta = new HashSet<receta>();
        }

        [Key]
        public int id_usuario { get; set; }

        [StringLength(250)]
        public string nom_usuario { get; set; }

        [StringLength(250)]
        public string ape_usuario { get; set; }

        [StringLength(300)]
        public string email_usuario { get; set; }

        [StringLength(15)]
        public string tel_usuario { get; set; }

        public DateTime? fecnac_usuario { get; set; }

        [StringLength(6)]
        public string sex_usuario { get; set; }

        [StringLength(255)]
        public string pass_usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta> dieta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_comentario> receta_comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta> receta { get; set; }
    }
}
