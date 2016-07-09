namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("usuario")]
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            dieta = new HashSet<dieta>();
            pedido = new HashSet<pedido>();
            receta_comentario = new HashSet<receta_comentario>();
            receta = new HashSet<receta>();
            favorito = new HashSet<favorito>();
        }

        [Key]
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "Nombre de usuario obligatorio")]
        [StringLength(250, ErrorMessage = "250 caracteres como máximo")]
        public string nom_usuario { get; set; }

        [Required]
        [StringLength(250)]
        public string ape_usuario { get; set; }

        [Required]
        [StringLength(300)]
        [Index(IsUnique = true)]
        public string email_usuario { get; set; }

        [Required]
        [StringLength(15)]
        public string tel_usuario { get; set; }

        public DateTime fecnac_usuario { get; set; }

        [Required]
        [StringLength(10)]
        public string sex_usuario { get; set; }

        [Required]
        [StringLength(255)]
        public string pass_usuario { get; set; }

        [Required]
        [StringLength(25)]
        public string tipo_usuario { get; set; }

        [Required]
        [StringLength(250)]
        public string dir_usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta> dieta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_comentario> receta_comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta> receta { get; set; }

        //NUEVO
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<favorito> favorito { get; set; }



        public List<usuario> Listar()
        {
            var usuarios = new List<usuario>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    usuarios = dbwanlla.usuario
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuarios;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    grilla.Inicializar();

                    var query = db.usuario.Where(x => x.id_usuario > 0);

                    //ordenar por columnas
                    if (grilla.columna == "id_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.id_usuario)
                            : query.OrderBy(x => x.id_usuario);
                    }
                    if (grilla.columna == "nom_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nom_usuario)
                            : query.OrderBy(x => x.nom_usuario);
                    }
                    if (grilla.columna == "ape_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.ape_usuario)
                            : query.OrderBy(x => x.ape_usuario);
                    }
                    if (grilla.columna == "email_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.email_usuario)
                            : query.OrderBy(x => x.email_usuario);
                    }
                    if (grilla.columna == "tel_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tel_usuario)
                            : query.OrderBy(x => x.tel_usuario);
                    }
                    if (grilla.columna == "fecnac_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fecnac_usuario)
                            : query.OrderBy(x => x.fecnac_usuario);
                    }
                    if (grilla.columna == "sex_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.sex_usuario)
                            : query.OrderBy(x => x.sex_usuario);
                    }
                    if (grilla.columna == "tipo_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipo_usuario)
                            : query.OrderBy(x => x.tipo_usuario);
                    }
                    if (grilla.columna == "dir_usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.dir_usuario)
                            : query.OrderBy(x => x.dir_usuario);
                    }

                    var usuarios = query.Skip(grilla.pagina).Take(grilla.limite).ToList();

                    var total = query.Count();

                    //enviamos a la grilla
                    grilla.SetData(
                        from u in usuarios
                        select new
                        {
                            u.id_usuario,
                            u.nom_usuario,
                            u.ape_usuario,
                            u.email_usuario,
                            u.tel_usuario,
                            u.fecnac_usuario,
                            u.sex_usuario,
                            u.tipo_usuario,
                            u.dir_usuario
                        },
                        total
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grilla.responde();
        }

        public List<usuario> buscar(string buscar)
        {
            var usuarios = new List<usuario>();

            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    if (id_usuario == 0)
                    {
                        usuarios = dbwanlla.usuario
                                .Where(x => x.nom_usuario.Contains(buscar)
                                        || x.ape_usuario.Contains(buscar)
                                        || x.email_usuario.Contains(buscar)
                                        || x.sex_usuario.Contains(buscar)
                                        || x.tipo_usuario.Contains(buscar))
                                .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuarios;
        }

        public void registrar()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    this.pass_usuario = BCrypt.Net.BCrypt.HashPassword(this.pass_usuario, BCrypt.Net.BCrypt.GenerateSalt());
                    db.Entry(this).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarCorreo()
        {
            bool estado = false;
            try
            {
                using (var db = new db_wanlla())
                {
                    var validar = db.usuario
                                    .Where(x => x.email_usuario == (this.email_usuario))
                                    .SingleOrDefault();

                    if (validar == null)
                    {
                        estado = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        public ResponseModel acceder(string email, string pass)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_wanlla())
                {
                    var usu = db.usuario.Where(x => x.email_usuario == email)
                                            .SingleOrDefault();

                    if (usu != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(pass, usu.pass_usuario))
                        {
                            SessionHelper.AddUserToSession(usu.id_usuario.ToString());
                            SessionHelper.CrearSesion(usu.id_usuario, usu.tipo_usuario, (usu.nom_usuario + " " + usu.ape_usuario));
                            rm.SetResponse(true);
                        }
                        else
                        {
                            rm.SetResponse(false, "Contraseña incorrecta");
                        }
                    }
                    else
                    {
                        rm.SetResponse(false, "El usuario no existe");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return rm;
        }

        public ResponseModel accederAdmin(string email, string pass)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new db_wanlla())
                {
                    var usu = db.usuario.Where(x => x.email_usuario == email)
                                            .SingleOrDefault();

                    if (usu != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(pass, usu.pass_usuario))
                        {
                            if (usu.tipo_usuario == "Admin")
                            {
                                SessionHelper.AddUserToSession(usu.id_usuario.ToString());
                                SessionHelper.CrearSesion(usu.id_usuario, usu.tipo_usuario, (usu.nom_usuario + " " + usu.ape_usuario));
                                rm.SetResponse(true);
                            }
                            else
                            {
                                rm.SetResponse(false, "Sin privilegios suficientes");
                            }
                        }
                        else
                        {
                            rm.SetResponse(false, "Contraseña incorrecta");
                        }
                    }
                    else
                    {
                        rm.SetResponse(false, "El usuario no existe");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rm;
        }

        /// <summary>
        /// Cambiar password del Usuario
        /// </summary>
        public void cambiarPass()
        {
            try
            {
                using (var db = new db_wanlla())
                {
                    this.pass_usuario = BCrypt.Net.BCrypt.HashPassword(this.pass_usuario, BCrypt.Net.BCrypt.GenerateSalt());
                    db.Entry(this).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[,] Consulta()
        {
            string[,] usuarios;
            try
            {
                using (var db = new db_wanlla())
                {
                    var cat = (from usuario in db.usuario
                               group usuario by new
                               {
                                   usuario.sex_usuario
                               } into g
                               select new
                               {
                                   Sexo = g.Key.sex_usuario,
                                   Cantidad = g.Count(p => p.sex_usuario != null)
                               }).ToList();
                    usuarios = new string[cat.Count(), 2];
                    int count = 0;
                    foreach (var c in cat)
                    {
                        usuarios[count, 0] = Convert.ToString(c.Sexo);
                        usuarios[count, 1] = Convert.ToString(c.Cantidad);
                        count++;
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
