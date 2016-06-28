namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    [Table("pedido")]
    public partial class pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido()
        {
            pedido_producto = new HashSet<pedido_producto>();
        }

        [Key]
        public int id_pedido { get; set; }

        public int id_usuario { get; set; }

        public DateTime fec_pedido { get; set; }

        [Required]
        [StringLength(50)]
        public string est_pedido { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_producto> pedido_producto { get; set; }

        public dieta obtenerDieta(int id, int id_usuario)
        {
            var dietas = new dieta();
            try
            {
                using (var db = new db_wanlla())
                {
                    dietas = db.dieta
                            .Include("dieta_receta.receta.ingrediente_receta.ingrediente")
                            .Where(x => x.id_dieta == id && x.id_usuario == id_usuario)
                            .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dietas;
        }
    }
}
