namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
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

        public List<receta> Listar(int cont)
        {
            var recetas = new List<receta>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    recetas = dbwanlla.receta.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recetas;
        }

        public receta Obtener(int id) //retornar es un objeto
        {
            var recetas = new receta();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    recetas = dbwanlla.receta.Include("ingrediente")
                        .Include("ingrediente_receta")
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
        public List<receta> buscar(string criterio)
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
                                .Where(x => x.nom_receta == criterio
                                        || x.des_receta == criterio
                                        || x.categoria.nom_categoria == criterio)
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
        /// Mantenimiento tabla Receta: Agregar / Actulizar
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
    }
}
