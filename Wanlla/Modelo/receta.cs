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
                using (var db = new db_wanlla())
                {
                    recetas = db.receta.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return recetas;
        }

        public List<receta> Listar(int cont)
        {
            var recetas = new List<receta>();
            try
            {
                using (var db = new db_wanlla())
                {
                    recetas = db.receta.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return recetas;
        }

        public receta Obtener(int id) //retornar es un objeto
        {
            var recetas = new receta();
            try
            {
                using (var db = new db_wanlla())
                {
                    recetas = db.receta.Include("categoria")
                        //.Include("PRODUCTO.NOMBRE")
                        .Where(x => x.id_receta == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
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
        public List<receta> buscar(string nom_receta, string des_receta, int time_receta)
        {
            var recetas = new List<receta>();

            try
            {
                using (var db = new db_wanlla())
                {
                    if (id_receta == 0)
                    {
                        recetas = db.receta
                                .Where(x => x.nom_receta.Contains(nom_receta) || x.des_receta.Contains(des_receta) || x.time_receta == time_receta)
                                .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
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
                using (var db = new db_wanlla())
                {
                    if (this.id_receta > 0)
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
        /// Eliminar receta
        /// </summary>
        public void eliminar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
