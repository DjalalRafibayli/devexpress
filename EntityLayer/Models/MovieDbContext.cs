using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityLayer.Models
{
    public partial class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieBanner> MovieBanners { get; set; } = null!;
        public virtual DbSet<MovieCategory> MovieCategories { get; set; } = null!;
        public virtual DbSet<SiteMenu> SiteMenus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=MovieDb;integrated security = true;MultipleActiveResultSets=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_Movies_MovieCategory");
            });

            modelBuilder.Entity<MovieBanner>(entity =>
            {
                entity.ToTable("MovieBanner");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<MovieCategory>(entity =>
            {
                entity.ToTable("MovieCategory");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<SiteMenu>(entity =>
            {
                entity.ToTable("SiteMenu");

                entity.Property(e => e.Action).HasMaxLength(150);

                entity.Property(e => e.Area).HasMaxLength(150);

                entity.Property(e => e.Controller).HasMaxLength(150);

                entity.Property(e => e.DescriptionAz)
                    .HasMaxLength(150)
                    .HasColumnName("Description_Az");

                entity.Property(e => e.DescriptionEn)
                    .HasMaxLength(150)
                    .HasColumnName("Description_En");

                entity.Property(e => e.DescriptionRu)
                    .HasMaxLength(150)
                    .HasColumnName("Description_Ru");

                entity.Property(e => e.DescriptionTr)
                    .HasMaxLength(150)
                    .HasColumnName("Description_Tr");

                entity.Property(e => e.HtmlAz)
                    .HasColumnType("text")
                    .HasColumnName("Html_Az");

                entity.Property(e => e.HtmlEn)
                    .HasColumnType("text")
                    .HasColumnName("Html_En");

                entity.Property(e => e.HtmlRu)
                    .HasColumnType("text")
                    .HasColumnName("Html_Ru");

                entity.Property(e => e.HtmlTr)
                    .HasColumnType("text")
                    .HasColumnName("Html_Tr");

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.Img).HasMaxLength(250);

                entity.Property(e => e.Link).HasMaxLength(150);

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MetaDescriptionAz)
                    .HasMaxLength(150)
                    .HasColumnName("MetaDescription_Az");

                entity.Property(e => e.MetaDescriptionEn)
                    .HasMaxLength(150)
                    .HasColumnName("MetaDescription_En");

                entity.Property(e => e.MetaDescriptionRu)
                    .HasMaxLength(150)
                    .HasColumnName("MetaDescription_Ru");

                entity.Property(e => e.MetaDescriptionTr)
                    .HasMaxLength(150)
                    .HasColumnName("MetaDescription_Tr");

                entity.Property(e => e.MetaKeywordAz)
                    .HasMaxLength(150)
                    .HasColumnName("MetaKeyword_Az");

                entity.Property(e => e.MetaKeywordEn)
                    .HasMaxLength(150)
                    .HasColumnName("MetaKeyword_En");

                entity.Property(e => e.MetaKeywordRu)
                    .HasMaxLength(150)
                    .HasColumnName("MetaKeyword_Ru");

                entity.Property(e => e.MetaKeywordTr)
                    .HasMaxLength(150)
                    .HasColumnName("MetaKeyword_Tr");

                entity.Property(e => e.NameAz)
                    .HasMaxLength(250)
                    .HasColumnName("Name_Az");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(250)
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameRu)
                    .HasMaxLength(250)
                    .HasColumnName("Name_Ru");

                entity.Property(e => e.NameTr)
                    .HasMaxLength(250)
                    .HasColumnName("Name_Tr");

                entity.Property(e => e.PermalinkAz)
                    .HasMaxLength(150)
                    .HasColumnName("Permalink_Az");

                entity.Property(e => e.PermalinkEn)
                    .HasMaxLength(150)
                    .HasColumnName("Permalink_En");

                entity.Property(e => e.PermalinkRu)
                    .HasMaxLength(150)
                    .HasColumnName("Permalink_Ru");

                entity.Property(e => e.PermalinkTr)
                    .HasMaxLength(150)
                    .HasColumnName("Permalink_Tr");

                entity.Property(e => e.SmallImg).HasMaxLength(250);

                entity.Property(e => e.TextAz)
                    .HasColumnType("text")
                    .HasColumnName("Text_Az");

                entity.Property(e => e.TextEn)
                    .HasColumnType("text")
                    .HasColumnName("Text_En");

                entity.Property(e => e.TextRu)
                    .HasColumnType("text")
                    .HasColumnName("Text_Ru");

                entity.Property(e => e.TextTr)
                    .HasColumnType("text")
                    .HasColumnName("Text_Tr");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
