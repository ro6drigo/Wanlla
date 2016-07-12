namespace LibraryToErase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class receta_establecimiento
    {
        [Key]
        public int id_recetaestablecimiento { get; set; }

        public int? id_receta { get; set; }

        public int? id_establecimiento { get; set; }

        [StringLength(50)]
        public string precio_receta { get; set; }

        public virtual establecimiento establecimiento { get; set; }

        public virtual receta receta { get; set; }
    }
}
