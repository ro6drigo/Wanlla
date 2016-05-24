namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class db_wanlla : DbContext
    {
        public db_wanlla()
            : base("name=db_wanlla")
        {
        }

        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<dieta> dieta { get; set; }
        public virtual DbSet<dieta_receta> dieta_receta { get; set; }
        public virtual DbSet<distribuidor> distribuidor { get; set; }
        public virtual DbSet<ingrediente> ingrediente { get; set; }
        public virtual DbSet<ingrediente_receta> ingrediente_receta { get; set; }
        public virtual DbSet<marca> marca { get; set; }
        public virtual DbSet<paso_receta> paso_receta { get; set; }
        public virtual DbSet<pedido> pedido { get; set; }
        public virtual DbSet<pedido_producto> pedido_producto { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<receta> receta { get; set; }
        public virtual DbSet<receta_comentario> receta_comentario { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categoria>()
                .Property(e => e.nom_categoria)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .Property(e => e.img_categoria)
                .IsUnicode(false);

            modelBuilder.Entity<dieta>()
                .Property(e => e.nom_dieta)
                .IsUnicode(false);

            modelBuilder.Entity<dieta>()
                .HasMany(e => e.dieta_receta)
                .WithRequired(e => e.dieta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<distribuidor>()
                .Property(e => e.nom_distribuidor)
                .IsUnicode(false);

            modelBuilder.Entity<distribuidor>()
                .Property(e => e.email_distribuidor)
                .IsUnicode(false);

            modelBuilder.Entity<distribuidor>()
                .HasMany(e => e.producto)
                .WithRequired(e => e.distribuidor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingrediente>()
                .Property(e => e.nom_ingrediente)
                .IsUnicode(false);

            modelBuilder.Entity<ingrediente>()
                .Property(e => e.tipo_ingrediente)
                .IsUnicode(false);

            modelBuilder.Entity<ingrediente>()
                .HasMany(e => e.ingrediente_receta)
                .WithRequired(e => e.ingrediente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingrediente>()
                .HasMany(e => e.producto)
                .WithRequired(e => e.ingrediente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingrediente_receta>()
                .Property(e => e.umed_ingrediente)
                .IsUnicode(false);

            modelBuilder.Entity<marca>()
                .Property(e => e.nom_marca)
                .IsUnicode(false);

            modelBuilder.Entity<marca>()
                .HasMany(e => e.producto)
                .WithRequired(e => e.marca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<paso_receta>()
                .Property(e => e.des_paso)
                .IsUnicode(false);

            modelBuilder.Entity<paso_receta>()
                .Property(e => e.img_paso)
                .IsUnicode(false);

            modelBuilder.Entity<pedido>()
                .Property(e => e.est_pedido)
                .IsUnicode(false);

            modelBuilder.Entity<pedido>()
                .HasMany(e => e.pedido_producto)
                .WithRequired(e => e.pedido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.des_producto)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.umed_producto)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .HasMany(e => e.pedido_producto)
                .WithRequired(e => e.producto)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.dieta_receta)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta>()
                .HasMany(e => e.ingrediente_receta)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta>()
                .HasMany(e => e.paso_receta)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta>()
                .HasMany(e => e.receta_comentario)
                .WithRequired(e => e.receta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receta>()
                .HasMany(e => e.usuario)
                .WithMany(e => e.receta)
                .Map(m => m.ToTable("favorito").MapLeftKey("id_receta").MapRightKey("id_usuario"));

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
                .HasMany(e => e.dieta)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.pedido)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.receta_comentario)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
