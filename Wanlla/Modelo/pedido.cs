namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
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

        [Required]
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
                    //SELECT  ir.id_ingrediente,
                    //  i.nom_ingrediente,
                    //  SUM(ir.cant_ingrediente * dr.cant_persona) AS cant_ingrediente,
                    //        ir.umed_ingrediente
                    //FROM ingrediente_receta AS ir
                    //JOIN receta AS r ON r.id_receta = ir.id_receta
                    //JOIN dieta_receta AS dr ON dr.id_receta = r.id_receta
                    //JOIN ingrediente AS i ON i.id_ingrediente = ir.id_ingrediente
                    //WHERE dr.id_dieta = id
                    //GROUP BY ir.id_ingrediente, i.nom_ingrediente, ir.umed_ingrediente

                    //PRIMER INTENTO :D
                    //var con = (from ir in db.ingrediente_receta
                    //           join r in db.receta on ir.id_receta equals r.id_receta
                    //           join dr in db.dieta_receta on r.id_receta equals dr.id_receta
                    //           join i in db.ingrediente on ir.id_ingrediente equals i.id_ingrediente
                    //           where dr.id_dieta == id
                    //           group ir by new { ir.id_ingrediente, i.nom_ingrediente, ir.umed_ingrediente, dr.cant_persona } into g
                    //           select new
                    //           {
                    //               id_ingrediente = g.Key.id_ingrediente,
                    //               nom_ingrediente = g.Key.nom_ingrediente,
                    //               cant_ingrediente = g.Sum(t => (t.cant_ingrediente * g.Key.cant_persona)),
                    //               umed_ingrediente = g.Key.umed_ingrediente
                    //           }).ToList();

                    var con = (from ir in db.ingrediente_receta
                              join dr in db.dieta_receta on ir.receta.id_receta equals dr.id_receta
                              where dr.id_dieta == id
                              group new { ir, ir.ingrediente, dr } by new
                              {
                                  ir.id_ingrediente,
                                  ir.ingrediente.nom_ingrediente,
                                  ir.umed_ingrediente
                              } into g
                              select new
                              {
                                  g.Key.id_ingrediente,
                                  g.Key.nom_ingrediente,
                                  cant_ingrediente = (decimal?)g.Sum(p => p.ir.cant_ingrediente * p.dr.cant_persona),
                                  g.Key.umed_ingrediente
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

        public List<producto> obtenerProductosIng(int id_ingrediente)
        {
            var producto = new List<producto>();

            try
            {
                using (var db = new db_wanlla())
                {
                    producto =  db.producto
                                .Include("ingrediente")
                                .Include("marca")
                                .Include("distribuidor")
                                .Where(x => x.id_ingrediente == id_ingrediente)
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return producto;
        }

        public int obtenerCantPro (decimal necesario, decimal unidad)
        {
            int cantidad = 1;
            decimal parcial = unidad;

            while (parcial < necesario)
            {
                parcial += unidad;
                cantidad ++;
            }

            return cantidad;
        }

        public void crearPedido()
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
                throw ex;
            }
        }

        public bool validarPedido(int id_pedido)
        {
            bool estado = false;
            int id_usu = SessionHelper.Leer<int>("id_usuario");

            try
            {
                using (var db = new db_wanlla())
                {
                    var pedido = db.pedido
                                    .Where(x => x.id_pedido == id_pedido && x.est_pedido == "En Espera" && x.id_usuario == id_usu)
                                    .SingleOrDefault();

                    estado = (pedido != null) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return estado;
        }

        public bool validarDieta(int id_dieta)
        {
            bool estado = false;
            int id_usu = SessionHelper.Leer<int>("id_usuario");

            try
            {
                using (var db = new db_wanlla())
                {
                    var dieta = db.dieta
                                    .Where(x => x.id_dieta == id_dieta && x.id_usuario == id_usu)
                                    .SingleOrDefault();

                    estado = (dieta != null) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return estado;
        }
    }
}
