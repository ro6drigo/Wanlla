namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("ingrediente")]
    public partial class ingrediente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ingrediente()
        {
            ingrediente_receta = new HashSet<ingrediente_receta>();
            producto = new HashSet<producto>();
        }

        [Key]
        public int id_ingrediente { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_ingrediente { get; set; }

        [Required]
        [StringLength(20)]
        public string tipo_ingrediente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingrediente_receta> ingrediente_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto> producto { get; set; }

        /// <summary>
        /// Listar todos los ingredientes sin filtro
        /// </summary>
        /// <returns></returns>
        public List<ingrediente> listar()
        {
            var ingredientes = new List<ingrediente>();
            try
            {
                using (var db = new db_wanlla())
                {
                    ingredientes = db.ingrediente.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ingredientes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grilla"></param>
        /// <returns></returns>
        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {

            try
            {
                using (var db = new db_wanlla())
                {
                    grilla.Inicializar();

                    var query = db.ingrediente.Where(x => x.id_ingrediente > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_ingrediente")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_ingrediente)
                            : query.OrderBy(x => x.id_ingrediente);
                    }
                    if (grilla.columna == "nom_ingrediente")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nom_ingrediente)
                            : query.OrderBy(x => x.nom_ingrediente);
                    }
                    if (grilla.columna == "tipo_ingrediente")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipo_ingrediente)
                            : query.OrderBy(x => x.tipo_ingrediente);
                    }

                    var ingredientes = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from t in ingredientes
                        select new
                        {
                            t.id_ingrediente,
                            t.nom_ingrediente,
                            t.tipo_ingrediente
                        },
                        total
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return grilla.responde();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ingrediente Obtener(int id) //retornar es un objeto
        {
            var ingredientes = new ingrediente();
            try
            {
                using (var db = new db_wanlla())
                {
                    ingredientes = db.ingrediente.Include("PRODUCTO")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.id_ingrediente == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ingredientes;
        }
        /// <summary>
        /// Buscar Ingredientes
        /// </summary>
        /// <param name="nom_ingrediente">Nombre del ingrediente</param>
        /// <param name="tipo_ingrediente">Tipo de ingrediente</param>
        /// <returns></returns>
        public List<ingrediente> buscar(string nom_ingrediente, string tipo_ingrediente)
        {
            var ingredientes = new List<ingrediente>();

            try
            {
                using (var db = new db_wanlla())
                {
                    if (id_ingrediente == 0)
                    {
                        ingredientes = db.ingrediente
                                .Where(x => x.nom_ingrediente.Contains(nom_ingrediente) || x.tipo_ingrediente == tipo_ingrediente)
                                .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return ingredientes;
        }

        /// <summary>
        /// Mantenimiento Tabla Ingredientes: Agregar / Actualizar
        /// </summary>
        public void mantenimiento()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    if (this.id_ingrediente > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Eliminar ingrediente
        /// </summary>
        public void eliminar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                    //db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
