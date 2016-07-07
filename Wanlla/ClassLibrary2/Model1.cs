namespace ClassLibrary2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model13")
        {
        }

        public virtual DbSet<favorito> favorito { get; set; }
        public virtual DbSet<receta> receta { get; set; }
        public virtual DbSet<receta_comentario> receta_comentario { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<receta>()
                .HasMany(e => e.favorito)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta>()
                .HasMany(e => e.receta_comentario)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta_comentario>()
                .Property(e => e.texto_comentario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nom_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.ape_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.email_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.tel_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.sex_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.pass_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.tipo_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.favorito)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.receta_comentario)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
