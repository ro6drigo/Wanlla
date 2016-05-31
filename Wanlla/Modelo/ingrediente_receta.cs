namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ingrediente_receta
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_ingrediente { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_receta { get; set; }

        public decimal cant_ingrediente { get; set; }

        [StringLength(50)]
        public string umed_ingrediente { get; set; }

        public virtual ingrediente ingrediente { get; set; }

        public virtual receta receta { get; set; }
    }
}
