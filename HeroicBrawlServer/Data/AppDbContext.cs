using System.Diagnostics.CodeAnalysis;
using HeroicBrawlServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace HeroicBrawlServer.Data.Repositories
{
    public class AppDbContext : DbContext
    {

        private readonly ILoggerFactory _loggerFactory;

        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MapUsers(builder);
            MapHistories(builder);
        }

        private void MapUsers(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
            
            MapBaseEntityFields(builder.Entity<User>());
            
            builder.Entity<User>()
                   .Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.Pseudo)
                   .HasColumnName("pseudo")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.PasswordSalt)
                   .HasColumnName("password_salt")
                   .IsRequired();
        }

        private void MapHistories(ModelBuilder builder)
        {
            builder.Entity<History>().ToTable("histories");

            MapBaseEntityFields(builder.Entity<History>());

            builder.Entity<History>()
                   .Property(u => u.MapId)
                   .HasColumnName("map_id")
                   .IsRequired();
            builder.Entity<History>()
                   .Property(u => u.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();
            builder.Entity<History>()
                   .Property(u => u.Deaths)
                   .HasColumnName("deaths")
                   .IsRequired();
            builder.Entity<History>()
                   .Property(u => u.Kills)
                   .HasColumnName("kills")
                   .IsRequired();
            builder.Entity<History>()
                   .Property(u => u.Rank)
                   .HasColumnName("rank")
                   .IsRequired();
        }

        private void MapBaseEntityFields<T>(EntityTypeBuilder<T> entityBuilder) where T : BaseEntity
        {
            entityBuilder.Property(x => x.Id)
                         .HasColumnName("id")
                         .IsRequired();
            entityBuilder.Property(x => x.CreatedAt)
                         .HasColumnName("created_at")
                         .IsRequired();
            entityBuilder.Property(x => x.CreatedById)
                         .HasColumnName("created_by")
                         .IsRequired();
            entityBuilder.HasOne(x => x.CreatedBy)
                         .WithMany()
                         .HasForeignKey(x => x.CreatedById);
            entityBuilder.Property(x => x.UpdatedAt)
                         .HasColumnName("updated_at")
                         .IsRequired();
            entityBuilder.Property(x => x.UpdatedById)
                         .HasColumnName("updated_by")
                         .IsRequired();
            entityBuilder.HasOne(x => x.UpdatedBy)
                         .WithMany()
                         .HasForeignKey(x => x.UpdatedById);
        }
    }
}
