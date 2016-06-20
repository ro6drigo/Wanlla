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

        public List<categoria> buscar(string criterio)
        {
            var categorias = new List<categoria>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (id_categoria == 0)
                    {
                        categorias = dbwanlla.categoria
                                .Where(x => x.nom_categoria.Contains(criterio) || x.img_categoria.Contains(criterio))
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

        public ResponseModel GuardarFoto(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    dbwanlla.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = dbwanlla.Entry(this);
                    eUsuario.State = EntityState.Modified;
                    //Obviar campos o ignorar en la actualización
                    if (Foto != null)
                    {
                        String archivo = Path.GetFileName(Foto.FileName);//Path.GetExtension(Foto.FileName);

                        //Nombre de imagen en forma aleatoria
                        //String archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);

                        //Colocar la ruta donde se grabará
                        Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                        //enviar al modelo el nombre del archivo
                        this.img_categoria = archivo;
                    }
                    else eUsuario.Property(x => x.img_categoria).IsModified = false; // el campo no es obligatorio

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
    }
}
