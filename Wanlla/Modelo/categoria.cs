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

    [Table("categoria")]
    public partial class categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public categoria()
        {
            receta = new HashSet<receta>();
        }

        [Key]
        public int id_categoria { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_categoria { get; set; }

        [StringLength(2048)]
        public string img_categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta> receta { get; set; }

        public List<categoria> listar()
        {
            var categorias = new List<categoria>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    categorias = dbwanlla.categoria.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categorias;
        }

        public List<categoria> ListarCategoria()
        {
            var tipo = new List<categoria>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    tipo = dbwanlla.categoria.OrderBy(x => x.id_categoria).ToList();
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

                    var query = db.categoria.Where(x => x.id_categoria > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_categoria")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_categoria)
                            : query.OrderBy(x => x.id_categoria);
                    }
                    if (grilla.columna == "nom_categoria")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nom_categoria)
                            : query.OrderBy(x => x.nom_categoria);
                    }
                    if (grilla.columna == "img_categoria")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.img_categoria)
                            : query.OrderBy(x => x.img_categoria);
                    }

                    var categorias = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from c in categorias
                        select new
                        {
                            c.id_categoria,
                            c.nom_categoria,
                            c.img_categoria
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
        public categoria Obtener(int id) //retornar es un objeto
        {
            var categorias = new categoria();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    categorias = dbwanlla.categoria.Include("receta")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.id_categoria == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categorias;
        }

        public List<categoria> buscar(string buscar)
        {
            var categorias = new List<categoria>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (id_categoria == 0)
                    {
                        categorias = dbwanlla.categoria
                                    .Where(x => x.nom_categoria.Contains(buscar)
                                            || x.img_categoria.Contains(buscar))
                                    .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categorias;
        }

        public void mantenimiento()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (this.id_categoria > 0)
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

        public ResponseModel Guardar(HttpPostedFileBase Foto)

        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_wanlla())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;

                    var eCategoria = db.Entry(this);
                    if (this.id_categoria > 0)
                    {
                        eCategoria.State = EntityState.Modified;

                        if (Foto != null)
                        {
                            String archivo = Path.GetFileName(Foto.FileName); //*Path.GetExtension(Foto.FileName);

                            //String archivo = DateTime.Now.ToString("yyyyMMddMMss") + Path.GetFileName(Foto.FileName);

                            Foto.SaveAs(HttpContext.Current.Server.MapPath("~/images/" + archivo));


                            this.img_categoria = archivo;

                        }
                        else eCategoria.Property(x => x.img_categoria).IsModified = false;

                        db.SaveChanges();
                        rm.SetResponse(true);
                    }
                    else
                    {
                        eCategoria.State = EntityState.Added;

                        if (Foto != null)
                        {
                            String archivo = Path.GetFileName(Foto.FileName); //*Path.GetExtension(Foto.FileName);

                            //String archivo = DateTime.Now.ToString("yyyyMMddMMss") + Path.GetFileName(Foto.FileName);

                            Foto.SaveAs(HttpContext.Current.Server.MapPath("~/images/" + archivo));


                            this.img_categoria = archivo;

                        }
                        else eCategoria.Property(x => x.img_categoria).IsModified = false;
                        db.SaveChanges();
                        rm.SetResponse(true);

                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
    }
}
