namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class dieta_receta
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_dieta { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_receta { get; set; }

        [Required(ErrorMessage = "La cantidad de personas en necesaria")]
        [Range(1, int.MaxValue, ErrorMessage = "No puede ser menor que cero")]
        public int cant_persona { get; set; }

        public virtual dieta dieta { get; set; }

        public virtual receta receta { get; set; }


        public dieta_receta obtener(int id_dieta, int id_receta)
        {
            var dieta_receta = new dieta_receta();

            try
            {
                using (var db = new db_wanlla())
                {
                    dieta_receta =  db.dieta_receta
                                    .Include("dieta")
                                    .Include("receta")
                                    .Where(x => x.id_dieta == id_dieta && x.id_receta == id_receta)
                                    .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dieta_receta;
        }

        public void guardar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void actualizar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarDietaReceta()
        {
            bool estado = false;
            try
            {
                using (var db = new db_wanlla())
                {
                    var validar = db.dieta_receta
                                    .Where(x => x.id_dieta == this.id_dieta && x.id_receta == this.id_receta)
                                    .SingleOrDefault();

                    if (validar == null)
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return estado;
        }

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
