using Microsoft.EntityFrameworkCore;
using Poseidon.API.Models;

namespace Poseidon.API.Data
{
    public partial class PoseidonContext : DbContext
    {
        public PoseidonContext()
        {
        }

        public PoseidonContext(DbContextOptions<PoseidonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BidList> BidList { get; set; }
        public virtual DbSet<CurvePoint> CurvePoint { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<RuleName> RuleName { get; set; }
        public virtual DbSet<Trade> Trade { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.\\SQLEXPRESS;Database=Poseidon;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BidList>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Benchmark)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.BidListDate).HasColumnType("datetime");

                entity.Property(e => e.Book)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Commentary)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CreationName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.DealName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.DealType)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionDate).HasColumnType("datetime");

                entity.Property(e => e.RevisionName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Security)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Side)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SourceListId)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Trader)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CurvePoint>(entity =>
            {
                entity.Property(e => e.AsOfDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Rating_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("Rating_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.FitchRating)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.MoodysRating)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SandPrating)
                    .HasColumnName("SandPRating")
                    .HasMaxLength(125)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RuleName>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("RuleName_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("RuleName_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Json)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SqlPart)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SqlStr)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Template)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Benchmark)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Book)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.BuyPrice).HasColumnType("money");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CreationName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.DealName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.DealType)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionDate).HasColumnType("datetime");

                entity.Property(e => e.RevisionName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Security)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SellPrice).HasColumnType("money");

                entity.Property(e => e.Side)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.SourceListId)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("datetime");

                entity.Property(e => e.Trader)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Users_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("Users_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.FullName)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(125)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}