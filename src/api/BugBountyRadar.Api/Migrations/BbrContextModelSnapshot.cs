﻿// <auto-generated />
using System;
using BugBountyRadar.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BugBountyRadar.Api.Migrations
{
    [DbContext(typeof(BbrContext))]
    partial class BbrContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BountyProgramTechnology", b =>
                {
                    b.Property<Guid>("ProgramsId")
                        .HasColumnType("uuid");

                    b.Property<int>("TechnologiesId")
                        .HasColumnType("integer");

                    b.HasKey("ProgramsId", "TechnologiesId");

                    b.HasIndex("TechnologiesId");

                    b.ToTable("BountyProgramTechnology");
                });

            modelBuilder.Entity("BugBountyRadar.Api.Domain.Entities.BountyProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("MinReward")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("BugBountyRadar.Api.Domain.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("BountyProgramTechnology", b =>
                {
                    b.HasOne("BugBountyRadar.Api.Domain.Entities.BountyProgram", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BugBountyRadar.Api.Domain.Entities.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
