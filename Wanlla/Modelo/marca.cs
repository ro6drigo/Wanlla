namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("marca")]
    public partial class marca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public marca()
        {
            producto = new HashSet<producto>();
        }

        [Key]
        public int id_marca { get; set; }

        [Required]
        [StringLength(250)]
        public string nom_marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto> producto { get; set; }

        public List<marca> Listar()
        {
            var marcas = new List<marca>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    marcas = dbwanlla.marca.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return marcas;
        }

        public List<marca> ListarMarca()
        {
            var tipo = new List<marca>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    tipo = dbwanlla.marca.OrderBy(x => x.id_marca).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipo;
        }

        public List<marca> Buscar(string criterio)
        {
            var marcas = new List<marca>();
            
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    marcas = dbwanlla.marca
                                .Where(x => x.nom_marca.Contains(criterio))
                                .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return marcas;
        }

        public marca Obtener(int id)
        {
            var marca = new marca();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    marca = dbwanlla.marca
                        .Where(x => x.id_marca == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return marca;
        }

        public void Guardar()
        {
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (this.id_marca > 0)
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
