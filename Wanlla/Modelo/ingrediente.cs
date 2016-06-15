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
