namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receta")]
    public partial class receta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public receta()
        {
            dieta_receta = new HashSet<dieta_receta>();
            ingrediente_receta = new HashSet<ingrediente_receta>();
            paso_receta = new HashSet<paso_receta>();
            receta_comentario = new HashSet<receta_comentario>();
            usuario = new HashSet<usuario>();
        }

        [Key]
        public int id_receta { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_receta { get; set; }

        [Required]
        [StringLength(250)]
        public string des_receta { get; set; }

        [Required]
        [StringLength(2048)]
        public string img_receta { get; set; }

        [StringLength(2048)]
        public string vid_receta { get; set; }

        [StringLength(7)]
        public string diff_receta { get; set; }

        public int time_receta { get; set; }

        public int id_categoria { get; set; }

        public virtual categoria categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta_receta> dieta_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingrediente_receta> ingrediente_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<paso_receta> paso_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_comentario> receta_comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario> usuario { get; set; }
    }
}
