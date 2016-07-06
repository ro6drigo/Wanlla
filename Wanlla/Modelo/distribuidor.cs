namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("distribuidor")]
    public partial class distribuidor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public distribuidor()
        {
            producto = new HashSet<producto>();
        }

        [Key]
        public int id_distribuidor { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_distribuidor { get; set; }

        [Required]
        [StringLength(300)]
        public string email_distribuidor { get; set; }

        [Required]
        [StringLength(15)]
        public string tel_distribuidor { get; set; }

        [Required]
        [StringLength(255)]
        public string pass_distribuidor { get; set; }

        [Required]
        [StringLength(50)]
        public string estado_distribuidor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto> producto { get; set; }

        public List<distribuidor> Listar()
        {
            var distribuidores = new List<distribuidor>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    distribuidores = dbwanlla.distribuidor.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return distribuidores;
        }

        public List<distribuidor> ListarDistribuidor()
        {
            var tipo = new List<distribuidor>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    tipo = dbwanlla.distribuidor.OrderBy(x => x.id_distribuidor).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipo;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    grilla.Inicializar();

                    var query = db.distribuidor.Where(x => x.id_distribuidor > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_distribuidor)
                            : query.OrderBy(x => x.id_distribuidor);
                    }
                    if (grilla.columna == "nom_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nom_distribuidor)
                            : query.OrderBy(x => x.nom_distribuidor);
                    }
                    if (grilla.columna == "email_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.email_distribuidor)
                            : query.OrderBy(x => x.email_distribuidor);
                    }
                    if (grilla.columna == "tel_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tel_distribuidor)
                            : query.OrderBy(x => x.tel_distribuidor);
                    }
                    if (grilla.columna == "estado_distribuidor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.estado_distribuidor)
                            : query.OrderBy(x => x.estado_distribuidor);
                    }

                    var distribuidores = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from d in distribuidores
                        select new
                        {
                            d.id_distribuidor,
                            d.nom_distribuidor,
                            d.email_distribuidor,
                            d.tel_distribuidor,
                            d.estado_distribuidor
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

        public List<distribuidor> Buscar(string buscar)
        {
            var distribuidores = new List<distribuidor>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    distribuidores = dbwanlla.distribuidor
                                .Where(x => x.nom_distribuidor.Contains(buscar) 
                                       || x.tel_distribuidor.Contains(buscar) 
                                       || x.email_distribuidor.Contains(buscar))
                                .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return distribuidores;
        }

        public distribuidor Obtener(int id)
        {
            var distribuidor = new distribuidor();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    distribuidor = dbwanlla.distribuidor
                        .Where(x => x.id_distribuidor == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return distribuidor;
        }

        public void Guardar()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (this.id_distribuidor > 0)
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

        public void Eliminar()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    dbwanlla.Entry(this).State = EntityState.Deleted;
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
