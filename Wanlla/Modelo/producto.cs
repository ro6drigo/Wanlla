namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("producto")]
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            pedido_producto = new HashSet<pedido_producto>();
        }

        [Key]
        public int id_producto { get; set; }

        public int id_ingrediente { get; set; }

        public int id_distribuidor { get; set; }

        public int id_marca { get; set; }

        [StringLength(250)]
        public string des_producto { get; set; }

        [StringLength(50)]
        public string umed_producto { get; set; }

        public decimal? cant_producto { get; set; }

        public decimal? precio_producto { get; set; }

        public virtual distribuidor distribuidor { get; set; }

        public virtual ingrediente ingrediente { get; set; }

        public virtual marca marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_producto> pedido_producto { get; set; }
    }
}
