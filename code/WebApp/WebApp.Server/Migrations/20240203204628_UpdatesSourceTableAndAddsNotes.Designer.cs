﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Server.Data;

#nullable disable

namespace WebApp.Server.Migrations
{
    [DbContext(typeof(CapstoneDbContext))]
    [Migration("20240203204628_UpdatesSourceTableAndAddsNotes")]
    partial class UpdatesSourceTableAndAddsNotes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApp.Server.Models.NoteTags", b =>
                {
                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NotesId")
                        .HasColumnType("int");

                    b.HasKey("TagName", "NotesId");

                    b.ToTable("NoteTags");
                });

            modelBuilder.Entity("WebApp.Server.Models.Notes", b =>
                {
                    b.Property<int>("NotesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotesId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.HasKey("NotesId");

                    b.HasIndex("SourceId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WebApp.Server.Models.Source", b =>
                {
                    b.Property<int>("SourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SourceId"));

                    b.Property<string>("AuthorFirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AuthorLastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SourceName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SourceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SourceId");

                    b.HasIndex("UserId");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("WebApp.Server.Models.Tag", b =>
                {
                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TagName");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WebApp.Server.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApp.Server.Models.Notes", b =>
                {
                    b.HasOne("WebApp.Server.Models.Source", "Source")
                        .WithMany("Notes")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("WebApp.Server.Models.Source", b =>
                {
                    b.HasOne("WebApp.Server.Models.User", "User")
                        .WithMany("Sources")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApp.Server.Models.Source", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("WebApp.Server.Models.User", b =>
                {
                    b.Navigation("Sources");
                });
#pragma warning restore 612, 618
        }
    }
}
