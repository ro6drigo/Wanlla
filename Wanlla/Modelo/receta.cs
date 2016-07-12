namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Web;

    [Table("receta")]
    public partial class receta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public receta()
        {
            dieta_receta = new HashSet<dieta_receta>();
            ingrediente_receta = new HashSet<ingrediente_receta>();
            paso_receta = new HashSet<paso_receta>();
            receta_comentario = new HashSet<receta_comentario>();
            usuario = new HashSet<usuario>();
            favorito = new HashSet<favorito>();
            receta_establecimiento = new HashSet<receta_establecimiento>();
        }

        [Key]
        public int id_receta { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_receta { get; set; }

        [Required]
        [StringLength(250)]
        public string des_receta { get; set; }

        [Required]
        [StringLength(2048)]
        public string img_receta { get; set; }

        [StringLength(2048)]
        public string vid_receta { get; set; }

        [StringLength(7)]
        public string diff_receta { get; set; }

        public int time_receta { get; set; }

        public int id_categoria { get; set; }

        public virtual categoria categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta_receta> dieta_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingrediente_receta> ingrediente_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<paso_receta> paso_receta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_comentario> receta_comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario> usuario { get; set; }

        //NUEVO
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<favorito> favorito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_establecimiento> receta_establecimiento { get; set; }

        public List<receta> Listar()
        {
            var recetas = new List<receta>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    recetas = dbwanlla.receta
                                        .Include("categoria")
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recetas;
        }

        public List<receta> listar(int count)
        {
            var recetas = new List<receta>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    recetas = dbwanlla.receta
                                        .Include("categoria")
                                        .OrderBy(x => x.id_receta)
                                        .Skip(((count-1)*6))
                                        .Take(6)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recetas;
        }

        public List<receta> listar(int count, string buscar)
        {
            var recetas = new List<receta>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    recetas = dbwanlla.receta
                                        .Include("categoria")
                                        .Where(x => x.nom_receta.Contains(buscar)
                                                || x.des_receta.Contains(buscar)
                                                || x.categoria.nom_categoria.Contains(buscar))
                                        .OrderBy(x => x.id_receta)
                                        .Skip(((count - 1) * 6))
                                        .Take((count * 6))
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recetas;
        }

        public int cantPaginador()
        {
            int cantidad = 0;
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    int aux = dbwanlla.receta
                                        .Include("categoria")
                                        .OrderBy(x => x.id_receta)
                                        .ToList()
                                        .Count;

                    if(aux % 6 != 0)
                    {
                        while (aux % 6 != 0)
                        {
                            aux++;
                        }
                    }

                    cantidad = aux / 6;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }

        public int cantPaginador(string buscar)
        {
            int cantidad = 0;
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    int aux = dbwanlla.receta
                                        .Include("categoria")
                                        .Where(x => x.nom_receta.Contains(buscar)
                                                || x.des_receta.Contains(buscar)
                                                || x.categoria.nom_categoria.Contains(buscar))
                                        .OrderBy(x => x.id_receta)
                                        .ToList()
                                        .Count;

                    if (aux % 6 != 0)
                    {
                        while (aux % 6 != 0)
                        {
                            aux++;
                        }
                    }

                    cantidad = aux / 6;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    grilla.Inicializar();

                    var query = db.receta.Where(x => x.id_receta > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_receta)
                            : query.OrderBy(x => x.id_receta);
                    }
                    if (grilla.columna == "nom_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nom_receta)
                            : query.OrderBy(x => x.nom_receta);
                    }
                    if (grilla.columna == "des_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.des_receta)
                            : query.OrderBy(x => x.des_receta);
                    }
                    if (grilla.columna == "img_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.img_receta)
                            : query.OrderBy(x => x.img_receta);
                    }
                    if (grilla.columna == "vid_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.vid_receta)
                            : query.OrderBy(x => x.vid_receta);
                    }
                    if (grilla.columna == "diff_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.diff_receta)
                            : query.OrderBy(x => x.diff_receta);
                    }
                    if (grilla.columna == "time_receta")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.time_receta)
                            : query.OrderBy(x => x.time_receta);
                    }
                    if (grilla.columna == "id_categoria")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_categoria)
                            : query.OrderBy(x => x.id_categoria);
                    }

                    var recetas = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from r in recetas
                        select new
                        {
                            r.id_receta,
                            r.nom_receta,
                            r.des_receta,
                            r.img_receta,
                            r.vid_receta,
                            r.diff_receta,
                            r.time_receta,
                            r.id_categoria
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

        public receta Obtener(int id) //retornar es un objeto
        {
            var recetas = new receta();
            try
            {
                using (var db = new db_wanlla())
                {
                    recetas = db.receta
                            .Include("categoria")
                            .Include("ingrediente_receta.ingrediente")
                            .Include("paso_receta")
                            //.Include("PRODUCTO.NOMBRE")
                            .Where(x => x.id_receta == id)
                            .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recetas;
        }
        /// <summary>
        /// Buscar recetas
        /// </summary>
        /// <param name="nom_receta">Nombre de la receta a buscar</param>
        /// <param name="des_receta">Descripción de la receta a buscar</param>
        /// <param name="time_receta">Tiempo de cocción de las recetas</param>
        /// <returns></returns>
        public List<receta> buscar(string buscar)
        {
            var recetas = new List<receta>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (id_receta == 0)
                    {
                        recetas = dbwanlla.receta
                                .Include("categoria")
                                .Where(x => x.nom_receta.Contains(buscar)
                                                || x.des_receta.Contains(buscar)
                                                || x.categoria.nom_categoria.Contains(buscar))
                                .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recetas;
        }
        /// <summary>
        /// Mantenimiento tabla Receta: Agregar / Actualizar
        /// </summary>
        public void mantenimiento()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (this.id_receta > 0)
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
        /// <summary>
        /// Eliminar receta
        /// </summary>
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

        public ResponseModel GuardarFoto(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    dbwanlla.Configuration.ValidateOnSaveEnabled = false;

                    var eReceta = dbwanlla.Entry(this);
                    eReceta.State = EntityState.Modified;
                    //Obviar campos o ignorar en la actualización
                    if (Foto != null)
                    {
                        String archivo = Path.GetFileName(Foto.FileName);//Path.GetExtension(Foto.FileName);

                        //Nombre de imagen en forma aleatoria
                        //String archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                        //Colocar la ruta donde se grabará
                        Foto.SaveAs(HttpContext.Current.Server.MapPath("~/images/" + archivo));

                        //enviar al modelo el nombre del archivo
                        this.img_receta = archivo;
                    }
                    else eReceta.Property(x => x.img_receta).IsModified = false; // el campo no es obligatorio

                    //if (this.NOMBREUSU == null) eUsuario.Property(x => x.NOMBREUSU).IsModified = false;

                    //if (this.PASSWORD == null) eUsuario.Property(x => x.PASSWORD).IsModified = false;

                    dbwanlla.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }

        public string[,] Consulta()
        {
            string[,] recetas;
            try
            {
                using (var db = new db_wanlla())
                {
                    var cat = (from c in db.categoria
                               join r in db.receta on c.id_categoria equals r.id_categoria into r_join
                               from r in r_join.DefaultIfEmpty()
                               group new { c, r } by new
                               {
                                   c.id_categoria,
                                   c.nom_categoria
                               } into g
                               select new
                               {
                                   id_categoria = (int?)g.Key.id_categoria,
                                   g.Key.nom_categoria,
                                   Cantidad = g.Count(p => p.r.id_categoria != null)
                               }).ToList();
                    recetas = new string[cat.Count(), 2];
                    int count = 0;
                    foreach (var c in cat)
                    {
                        recetas[count, 0] = Convert.ToString(c.nom_categoria);
                        recetas[count, 1] = Convert.ToString(c.Cantidad);
                        count++;
                    }
                }
                return recetas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
