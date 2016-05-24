namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class paso_receta
    {
        [Key]
        public int id_paso { get; set; }

        public int id_receta { get; set; }

        [Required]
        [StringLength(2500)]
        public string des_paso { get; set; }

        [StringLength(2048)]
        public string img_paso { get; set; }

        public virtual receta receta { get; set; }
    }
}