﻿// <auto-generated />
using System;
using DocumentApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocumentApp.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DocumentApp.Domain.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PatronimicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("DocumentApp.Domain.CitationIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Indexator")
                        .HasColumnType("int");

                    b.Property<Guid?>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("CitationIndex");
                });

            modelBuilder.Entity("DocumentApp.Domain.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId")
                        .IsUnique();

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("DocumentApp.Domain.Publication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PublicationType")
                        .HasColumnType("int");

                    b.Property<int>("PublishingYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("DocumentApp.Domain.Author", b =>
                {
                    b.HasOne("DocumentApp.Domain.Publication", "Publication")
                        .WithMany("Authors")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("DocumentApp.Domain.CitationIndex", b =>
                {
                    b.HasOne("DocumentApp.Domain.Publication", null)
                        .WithMany("Indices")
                        .HasForeignKey("PublicationId");
                });

            modelBuilder.Entity("DocumentApp.Domain.Conference", b =>
                {
                    b.HasOne("DocumentApp.Domain.Publication", "Publication")
                        .WithOne("Conference")
                        .HasForeignKey("DocumentApp.Domain.Conference", "PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("DocumentApp.Domain.Publication", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Conference");

                    b.Navigation("Indices");
                });
#pragma warning restore 612, 618
        }
    }
}
