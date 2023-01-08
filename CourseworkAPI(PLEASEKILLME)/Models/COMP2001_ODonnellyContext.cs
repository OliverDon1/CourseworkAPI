using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CourseworkAPI_PLEASEKILLME_.Models
{
    public partial class COMP2001_ODonnellyContext : DbContext
    {

        public COMP2001_ODonnellyContext()
        {
        }

        public COMP2001_ODonnellyContext(DbContextOptions<COMP2001_ODonnellyContext> options): base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(dbCreator != null)
                {
                    if (!dbCreator.CanConnect()) dbCreator.Create();
                    if (!dbCreator.HasTables()) dbCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual DbSet<Archive> Archives { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Trail> Trails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public DbSet<ViewContext> Comment { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Database=COMP2001_ODonnelly;User Id=ODonnelly; Password=YsqH802+; Encrypt =False ; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewContext>(c =>
            {
                c.HasNoKey();
                c.ToView("Comment");
            });
            modelBuilder.Entity<Archive>(entity =>
            {
                entity.ToTable("Archive", "CW2");

                entity.Property(e => e.ArchiveId).HasColumnName("archiveID");

                entity.Property(e => e.ArchiveDate)
                    .HasColumnType("date")
                    .HasColumnName("archiveDate");

                entity.Property(e => e.CommentDate)
                    .HasColumnType("date")
                    .HasColumnName("commentDate");

                entity.Property(e => e.CommentId).HasColumnName("commentID");

                entity.Property(e => e.CommentText)
                    .HasColumnType("text")
                    .HasColumnName("commentText");

                entity.Property(e => e.TrailId).HasColumnName("trailID");

                entity.Property(e => e.UserId).HasColumnName("userID");


                entity.HasOne(d => d.Trail)
                    .WithMany(p => p.Archives)
                    .HasForeignKey(d => d.TrailId)
                    .HasConstraintName("FK_trailID1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Archives)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_userID1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments", "CW2");

                entity.Property(e => e.CommentId).HasColumnName("commentID");

                entity.Property(e => e.CommentDate)
                    .HasColumnType("date")
                    .HasColumnName("commentDate");

                entity.Property(e => e.CommentText)
                    .HasColumnType("text")
                    .HasColumnName("commentText");

                entity.Property(e => e.TrailId).HasColumnName("trailID");

                entity.Property(e => e.UserId).HasColumnName("userID");

            });

            //modelBuilder.Entity<Trail>(entity =>
            //{
            //    entity.ToTable("Trails", "CW2");

            //    entity.Property(e => e.TrailId).HasColumnName("trailID");

            //    entity.Property(e => e.TrailLength).HasColumnName("trailLength");

            //    entity.Property(e => e.TrailName)
            //        .HasMaxLength(256)
            //        .IsUnicode(false)
            //        .HasColumnName("trailName");

            //    entity.Property(e => e.TrailRating).HasColumnName("trailRating");
            //});

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("Users", "CW2");

            //    entity.Property(e => e.UserId).HasColumnName("userID");

            //    entity.Property(e => e.Admin).HasColumnName("admin");

            //    entity.Property(e => e.SignupDate)
            //        .HasColumnType("date")
            //        .HasColumnName("signupDate");

            //    entity.Property(e => e.Username)
            //        .HasMaxLength(256)
            //        .IsUnicode(false)
            //        .HasColumnName("username");
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
