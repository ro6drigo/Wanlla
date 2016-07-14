namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class pedido_producto
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id Pedido")]
        public int id_pedido { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Seleccione un producto")]
        public int? id_producto { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int cant_producto { get; set; }

        public virtual pedido pedido { get; set; }

        public virtual producto producto { get; set; }

        public ResponseModel guardar()
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Added;
                    rm.SetResponse(true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rm;
        }

        public ResponseModel eliminar()
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_wanlla())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    rm.SetResponse(true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rm;
        }

        public pedido obtenerPedido(int id_pedido)
        {
            var pedido = new pedido();

            try
            {
                using (var db = new db_wanlla())
                {
                    pedido = db.pedido
                                .Include("pedido_producto.producto.distribuidor")
                                .Include("pedido_producto.producto.marca")
                                .Include("pedido_producto.producto.ingrediente")
                                .Include("usuario")
                                .Include("dieta")
                                .Where(x => x.id_pedido == id_pedido)
                                .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pedido;
        }

        public List<producto> obtenerProductosIng(int id_ingrediente)
        {
            var producto = new List<producto>();

            try
            {
                using (var db = new db_wanlla())
                {
                    producto = db.producto
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

        public string[,] obtenerIngRes(int id_dieta, int id_pedido)
        {
            string[,] ing_rec;

            try
            {
                using (var db = new db_wanlla())
                {
                    //SELECT  i.id_ingrediente,
		                  //  i.nom_ingrediente
                    //FROM ingrediente AS i
                    //JOIN ingrediente_receta AS ir ON ir.id_ingrediente = i.id_ingrediente
                    //JOIN receta AS r ON r.id_receta = ir.id_receta
                    //JOIN dieta_receta AS dr ON dr.id_receta = r.id_receta
                    //WHERE dr.id_dieta = 7 AND
                    //        i.id_ingrediente NOT IN(SELECT  p0.id_ingrediente
                    //                                    FROM pedido_producto AS pp0
                    //                                    JOIN producto AS p0 ON p0.id_producto = pp0.id_producto
                    //                                    WHERE pp0.id_pedido = 17
                    //                                )
                    //GROUP BY i.id_ingrediente, i.nom_ingrediente

                    var con = (from ir in db.ingrediente_receta
                               join dr in db.dieta_receta on ir.receta.id_receta equals dr.receta.id_receta
                               where dr.id_dieta == id_dieta && ! (from pp0 in db.pedido_producto
                                                            where pp0.id_pedido == id_pedido
                                                            select new
                                                            {
                                                                pp0.producto.id_ingrediente
                                                            }).Contains(new { id_ingrediente = ir.ingrediente.id_ingrediente })
                               group ir.ingrediente by new
                               {
                                   ir.ingrediente.id_ingrediente,
                                   ir.ingrediente.nom_ingrediente
                               } into g
                               select new
                               {
                                   g.Key.id_ingrediente,
                                   g.Key.nom_ingrediente
                               }).ToList();

                    ing_rec = new string[con.Count(), 2];

                    int count = 0;
                    foreach (var c in con)
                    {
                        ing_rec[count, 0] = Convert.ToString(c.id_ingrediente);
                        ing_rec[count, 1] = Convert.ToString(c.nom_ingrediente);

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

        public ingrediente obtenerIngrediente(int id_ingrediente)
        {
            var ingrediente = new ingrediente();

            try
            {
                using (var db = new db_wanlla())
                {
                    ingrediente = db.ingrediente
                                    .Include("producto.marca")
                                    .Include("producto.distribuidor")
                                    .Where(x => x.id_ingrediente == id_ingrediente)
                                    .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ingrediente;
        }

        public string[,] obtenerIngrediente(int id_ingrediente, int id_dieta)
        {
            string[,] ing_rec;

            try
            {
                using (var db = new db_wanlla())
                {
                    var con = (from ir in db.ingrediente_receta
                               join dr in db.dieta_receta on ir.receta.id_receta equals dr.id_receta
                               where dr.id_dieta == id_dieta && ir.ingrediente.id_ingrediente == id_ingrediente
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

        public int obtenerCantPro(decimal necesario, decimal unidad)
        {
            int cantidad = 1;
            decimal parcial = unidad;

            while (parcial < necesario)
            {
                parcial += unidad;
                cantidad++;
            }

            return cantidad;
        }
    }
}
