namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class receta_establecimiento
    {
        [Key]
        public int id_recetaestablecimiento { get; set; }

        public int? id_receta { get; set; }

        public int? id_establecimiento { get; set; }

        [StringLength(50)]
        public string precio_receta { get; set; }

        public virtual establecimiento establecimiento { get; set; }

        public virtual receta receta { get; set; }

        public List<receta_establecimiento> Listar()
        {
            var establecimientos = new List<receta_establecimiento>();
            try
            {
                using (var dbwanlla = new db_wanlla())
                {
                    establecimientos = dbwanlla.receta_establecimiento
                                        .Include("establecimiento")
                                        //.Where(x => x.id_receta == idreceta)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return establecimientos;
        }
    }
}
