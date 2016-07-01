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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dieta> dieta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_comentario> receta_comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta> receta { get; set; }


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
                throw;
            }
            return usuarios;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
            }
        }
    }
}
