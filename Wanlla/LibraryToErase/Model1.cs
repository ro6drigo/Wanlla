namespace LibraryToErase
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model15")
        {
        }

        public virtual DbSet<establecimiento> establecimiento { get; set; }
        public virtual DbSet<receta> receta { get; set; }
        public virtual DbSet<receta_establecimiento> receta_establecimiento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<establecimiento>()
                .Property(e => e.nom_establecimiento)
                .IsUnicode(false);

            modelBuilder.Entity<establecimiento>()
                .Property(e => e.lat_establecimiento)
                .IsUnicode(false);

            modelBuilder.Entity<establecimiento>()
                .Property(e => e.lon_establecimiento)
                .IsUnicode(false);

            modelBuilder.Entity<receta>()
                .Property(e => e.nom_receta)
                .IsUnicode(false);

            modelBuilder.Entity<receta>()
                .Property(e => e.des_receta)
                .IsUnicode(false);

            modelBuilder.Entity<receta>()
                .Property(e => e.img_receta)
                .IsUnicode(false);

            modelBuilder.Entity<receta>()
                .Property(e => e.vid_receta)
                .IsUnicode(false);

            modelBuilder.Entity<receta>()
                .Property(e => e.diff_receta)
                .IsUnicode(false);

            modelBuilder.Entity<receta_establecimiento>()
                .Property(e => e.precio_receta)
                .IsUnicode(false);
        }
    }
}
