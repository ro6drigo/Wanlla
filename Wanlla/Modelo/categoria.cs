namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
            }
        }
    }
}
