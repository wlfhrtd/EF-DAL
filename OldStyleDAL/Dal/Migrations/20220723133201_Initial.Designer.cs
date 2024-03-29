﻿// <auto-generated />
using System;
using Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220723133201_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dal.Entities.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("AllowChatSounds")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("AvatarURL");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<byte>("ContactPrivacyLevel")
                        .HasColumnType("tinyint");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("FileUploadQuotaInBytes")
                        .HasColumnType("int");

                    b.Property<int>("FileUploadsInBytes")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastActivityDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid?>("OldUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ProfileLastViewDate")
                        .HasColumnType("smalldatetime");

                    b.Property<byte>("ProfilePrivacyLevel")
                        .HasColumnType("tinyint");

                    b.Property<long>("ProfileViews")
                        .HasColumnType("bigint");

                    b.Property<string>("Province")
                        .HasMaxLength(65)
                        .IsUnicode(false)
                        .HasColumnType("varchar(65)");

                    b.Property<byte?>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValueSql("((3))");

                    b.Property<bool?>("ShowChatStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("WebSite")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ArtistId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Dal.Entities.ArtistSkill", b =>
                {
                    b.Property<int>("ArtistTalentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ArtistTalentID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("SkillLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((3))");

                    b.Property<string>("Styles")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("TalentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ArtistTalentId")
                        .HasName("PK__tmp_ms_x__A9AD4EAAFEE755FA");

                    b.HasIndex("ArtistId");

                    b.ToTable("ArtistSkill");
                });

            modelBuilder.Entity("Dal.Entities.ArtistSkill", b =>
                {
                    b.HasOne("Dal.Entities.Artist", "Artist")
                        .WithMany("ArtistSkills")
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("FK_ArtistSkill_Artist")
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Dal.Entities.Artist", b =>
                {
                    b.Navigation("ArtistSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
