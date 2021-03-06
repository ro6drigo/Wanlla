namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;
    using System.IO;
    using System.Web;

    [Table("favorito")]
    public partial class favorito
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_receta { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha { get; set; }

        public virtual receta receta { get; set; }

        public virtual usuario usuario { get; set; }

        //public virtual categoria categoria { get; set; }

        public List<favorito> Listar()
        {
            var favoritos = new List<favorito>();

            try
            {
                using (var db = new db_wanlla())
                {
                    favoritos = db.favorito.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return favoritos;
        }

        public List<favorito> Listar(int idusuario)
        {
            var favoritos = new List<favorito>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    favoritos = dbwanlla.favorito
                                        .Include("usuario")
                                        .Include("receta")
                                        .Where(x => x.id_usuario == idusuario)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return favoritos;
        }
        public void Guardar()
        {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Added;
                    db.SaveChanges();
                }
        }
        public void Eliminar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        //Reportes

        public string[,] RMasFavorito()
        {
            string[,] favoritos;
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    var favs = (from fav in dbwanlla.favorito
                                join re in dbwanlla.receta on fav.id_receta equals re.id_receta
                                //group new { fav, re } by new
                                //{
                                //    fav.id_receta
                                //} into g
                                group fav by new
                                {
                                    fav.id_receta
                                } into g
                                select new
                                {
                                    g.Key.id_receta,
                                    cantidad = (int?)g.Count(p => p.id_usuario != null),
                                    //nombre = receta.nom_receta + ""
                                }).ToList().OrderByDescending(c => c.cantidad);

                    favoritos = new string[favs.Count(), 2];

                    int count = 0;
                    foreach (var f in favs)
                    {
                        favoritos[count, 0] = Convert.ToString(f.id_receta);
                        favoritos[count, 1] = Convert.ToString(f.cantidad);
                        //favoritos[count, 3] = f.nombre;

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return favoritos;
        }
    }
}
