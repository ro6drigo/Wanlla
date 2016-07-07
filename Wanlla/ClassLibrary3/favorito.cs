namespace ClassLibrary3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("favorito")]
    public partial class favorito
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_receta { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha { get; set; }

        public virtual receta receta { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
