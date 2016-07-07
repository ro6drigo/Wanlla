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

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    grilla.Inicializar();

                    var query = db.producto
                        .Where(x => x.id_producto > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_producto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_producto)
                            : query.OrderBy(x => x.id_producto);
                    }
                    if (grilla.columna == "id_ingrediente")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_ingrediente)
                            : query.OrderBy(x => x.id_ingrediente);
                    }
                    if (grilla.columna == "id_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_distribuidor)
                            : query.OrderBy(x => x.id_distribuidor);
                    }
                    if (grilla.columna == "id_marca")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_marca)
                            : query.OrderBy(x => x.id_marca);
                    }
                    if (grilla.columna == "des_producto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.des_producto)
                            : query.OrderBy(x => x.des_producto);
                    }
                    if (grilla.columna == "umed_producto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.umed_producto)
                            : query.OrderBy(x => x.umed_producto);
                    }
                    if (grilla.columna == "cant_producto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.cant_producto)
                            : query.OrderBy(x => x.cant_producto);
                    }
                    if (grilla.columna == "precio_producto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.precio_producto)
                            : query.OrderBy(x => x.precio_producto);
                    }

                    var productos = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from p in productos
                        select new
                        {
                            p.id_producto,
                            p.id_ingrediente,
                            p.id_distribuidor,
                            p.id_marca,
                            p.des_producto,
                            p.umed_producto,
                            p.cant_producto,
                            p.precio_producto
                        },
                        total
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grilla.responde();
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

        public List<producto> buscar(string buscar)
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
                                .Where(x => x.ingrediente.nom_ingrediente.Contains(buscar)
                                        || x.distribuidor.nom_distribuidor.Contains(buscar)
                                        || x.marca.nom_marca.Contains(buscar)
                                        || x.des_producto.Contains(buscar)
                                        || x.umed_producto.Contains(buscar))
                                .ToList();
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
