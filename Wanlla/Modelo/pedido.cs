namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    [Table("pedido")]
    public partial class pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido()
        {
            pedido_producto = new HashSet<pedido_producto>();
        }

        [Key]
        public int id_pedido { get; set; }

        public int id_usuario { get; set; }

        public DateTime fec_pedido { get; set; }

        [Required]
        [StringLength(50)]
        public string est_pedido { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_producto> pedido_producto { get; set; }

        public dieta obtenerDieta(int id, int id_usuario)
        {
            var dietas = new dieta();
            try
            {
                using (var db = new db_wanlla())
                {
                    dietas = db.dieta
                            .Include("dieta_receta.receta.ingrediente_receta.ingrediente")
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

        public string[,] obtenerTotalIng(int id)
        {
            string[,] ing_rec;


            try
            {
                using (var db = new db_wanlla())
                {

                    var con = (from ir in db.ingrediente_receta
                               join r in db.receta on ir.id_receta equals r.id_receta
                               join dr in db.dieta_receta on r.id_receta equals dr.id_receta
                               join i in db.ingrediente on ir.id_ingrediente equals i.id_ingrediente
                               where dr.id_dieta == id
                               group ir by new { ir.id_ingrediente, i.nom_ingrediente, ir.umed_ingrediente, dr.cant_persona } into g
                               select new
                               {
                                   id_ingrediente = g.Key.id_ingrediente,
                                   nom_ingrediente = g.Key.nom_ingrediente,
                                   cant_ingrediente = g.Sum(t => (t.cant_ingrediente * g.Key.cant_persona)),
                                   umed_ingrediente = g.Key.umed_ingrediente
                               }).ToList();

                    ing_rec = new string[con.Count(), 4];

                    int count = 0;
                    foreach (var c in con)
                    {
                        ing_rec[count, 0] = Convert.ToString(c.id_ingrediente);
                        ing_rec[count, 1] = Convert.ToString(c.nom_ingrediente);
                        ing_rec[count, 2] = Convert.ToString(c.cant_ingrediente);
                        ing_rec[count, 3] = Convert.ToString(c.umed_ingrediente);

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ing_rec;
        }
    }
}
