namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    [Table("dieta")]
    public partial class dieta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dieta()
        {
            dieta_receta = new HashSet<dieta_receta>();
        }

        [Key]
        public int id_dieta { get; set; }

        public int id_usuario { get; set; }

        [Required]
        [StringLength(20)]
        public string nom_dieta { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta_receta> dieta_receta { get; set; }

        /// <summary>
        /// Listar todas las dietas del usuario
        /// </summary>
        /// <param name="id_usuario"> Id del usuario (logueado) actual en el sistema </param>
        /// <returns> Lista de elementos dieta </returns>
        public List<dieta> listar(int id_usuario)
        {
            var dietas = new List<dieta>();

            try
            {
                using (var db = new db_wanlla())
                {
                    dietas = db.dieta
                            .Where(x => x.id_usuario == id_usuario)
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return dietas;
        }

        /// <summary>
        /// Buscar dietas del usuario por nombre
        /// </summary>
        /// <param name="id_usuario"> Id del usuario (logueado) actual en el sistema </param>
        /// <param name="nom_dieta"> parametro de busqueda (nom_dieta) </param>
        /// <returns> Lista de dietas que coincidan con la busqueda </returns>
        public List<dieta> buscar(int id_usuario, string nom_dieta)
        {
            var dietas = new List<dieta>();

            try
            {
                using (var db = new db_wanlla())
                {
                    dietas = db.dieta
                            .Where(x => x.id_usuario == id_usuario && x.nom_dieta.Contains(nom_dieta))
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return dietas;
        }

        /// <summary>
        /// Manteniemiento (Agregar, Modificar) Dieta
        /// </summary>
        public void mantenimiento()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    if (this.id_dieta > 0)
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

        public dieta obtener(int id, int id_usuario) //retornar es un objeto
        {
            var dietas = new dieta();
            try
            {
                using (var db = new db_wanlla())
                {
                    dietas = db.dieta
                            .Where(x => x.id_dieta == id && x.id_usuario == id_usuario)
                            .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dietas;
        }

        /// <summary>
        /// Eliminar Dieta
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


        public dieta detalle(int id_dieta, int id_usuario)
        {
            var dieta_recetas = new dieta();
            try
            {
                using (var db = new db_wanlla())
                {
                    dieta_recetas = db.dieta
                                .Include("dieta_receta.receta")
                                .Where(x => x.id_dieta == id_dieta && x.id_usuario == id_usuario)
                                .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dieta_recetas;
        }
    }
}
