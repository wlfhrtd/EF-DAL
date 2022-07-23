using Microsoft.EntityFrameworkCore;
using Dal.Entities;


namespace Dal.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        private string connectionString = null;


        public ApplicationDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (connectionString == null)
            {
                base.OnConfiguring(options);
                return;
            }

            options.UseSqlServer(connectionString);
        }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<ArtistSkill> ArtistSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.AllowChatSounds)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AvatarURL");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.LastActivityDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProfileLastViewDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Province)
                    .HasMaxLength(65)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasDefaultValueSql("((3))");

                entity.Property(e => e.ShowChatStatus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.WebSite)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ArtistSkill>(entity =>
            {
                entity.HasKey(e => e.ArtistTalentId)
                    .HasName("PK__tmp_ms_x__A9AD4EAAFEE755FA");

                entity.ToTable("ArtistSkill");

                entity.Property(e => e.ArtistTalentId).HasColumnName("ArtistTalentID");

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SkillLevel).HasDefaultValueSql("((3))");

                entity.Property(e => e.Styles)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TalentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistSkills)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistSkill_Artist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
