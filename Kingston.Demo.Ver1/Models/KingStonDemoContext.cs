using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kingston.Demo.Ver1.Models
{
    public partial class KingStonDemoContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<IssueFiles> IssueFiles { get; set; }
        public virtual DbSet<IssueSub> IssueSub { get; set; }
        public virtual DbSet<TblCode> TblCode { get; set; }
        public KingStonDemoContext(DbContextOptions<KingStonDemoContext> options) : base(options) { }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KingStonDemo;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.KeyInDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.KeyInDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.KeyInUser).HasMaxLength(50);

                entity.Property(e => e.Pid).HasDefaultValueSql("((-1))");

                entity.Property(e => e.SubType).HasMaxLength(50);

                entity.Property(e => e.Tags).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<IssueFiles>(entity =>
            {
                entity.Property(e => e.FileName).HasMaxLength(300);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<IssueSub>(entity =>
            {
                entity.Property(e => e.Desc).HasMaxLength(300);

                entity.Property(e => e.KeyInDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.KeyInUser).HasMaxLength(50);
            });

            modelBuilder.Entity<TblCode>(entity =>
            {
                entity.ToTable("tblCode");

                entity.Property(e => e.Text).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
