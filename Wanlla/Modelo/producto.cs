namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("producto")]
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            pedido_producto = new HashSet<pedido_producto>();
        }

        [Key]
        public int id_producto { get; set; }

        public int id_ingrediente { get; set; }

        public int id_distribuidor { get; set; }

        public int id_marca { get; set; }

        [Required]
        [StringLength(250)]
        public string des_producto { get; set; }

        [StringLength(50)]
        public string umed_producto { get; set; }

        public decimal cant_producto { get; set; }

        public decimal precio_producto { get; set; }

        public virtual distribuidor distribuidor { get; set; }

        public virtual ingrediente ingrediente { get; set; }

        public virtual marca marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_producto> pedido_producto { get; set; }

        public List<producto> listar()
        {
            var productos = new List<producto>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    productos = dbwanlla.producto
                                        .Include("ingrediente")
                                        .Include("distribuidor")
                                        .Include("marca")
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productos;
        }

        public producto Obtener(int id) //retornar es un objeto
        {
            var productos = new producto();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    productos = dbwanlla.producto.Include("pedido_producto")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.id_producto == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productos;
        }

        public List<producto> buscar(string criterio)
        {
            var productos = new List<producto>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (id_producto == 0)
                    {
                        productos = dbwanlla.producto
                                .Include("ingrediente").Include("distribuidor").Include("marca")
                                .Where(x => x.ingrediente.nom_ingrediente == criterio
                                || x.distribuidor.nom_distribuidor == criterio
                                || x.marca.nom_marca == criterio
                                || x.des_producto == criterio
                                || x.umed_producto == criterio)
                                .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productos;
        }

        public void mantenimiento()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (this.id_producto > 0)
                    {
                        dbwanlla.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        dbwanlla.Entry(this).State = EntityState.Added;
                    }
                    dbwanlla.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminar()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    dbwanlla.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                    dbwanlla.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
