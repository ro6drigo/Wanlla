namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    public partial class pedido_producto
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id Pedido")]
        public int id_pedido { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Seleccione un producto")]
        public int? id_producto { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int cant_producto { get; set; }

        public virtual pedido pedido { get; set; }

        public virtual producto producto { get; set; }

        public void guardar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
