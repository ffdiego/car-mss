﻿// <auto-generated />
using System;
using LeilaoService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeilaoService.Data.Migrations
{
    [DbContext(typeof(LeilaoDbContext))]
    partial class LeilaoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LeilaoService.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<string>("Cor")
                        .HasColumnType("text");

                    b.Property<string>("Fabrica")
                        .HasColumnType("text");

                    b.Property<Guid>("LeilaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Modelo")
                        .HasColumnType("text");

                    b.Property<int>("Quilometragem")
                        .HasColumnType("integer");

                    b.Property<string>("URLImagem")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LeilaoId")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LeilaoService.Leilao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FinalizadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LanceAtualMaisAlto")
                        .HasColumnType("integer");

                    b.Property<int>("PrecoReserva")
                        .HasColumnType("integer");

                    b.Property<int?>("QuantidadeVendida")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Vencedor")
                        .HasColumnType("text");

                    b.Property<string>("Vendedor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Leiloes");
                });

            modelBuilder.Entity("LeilaoService.Item", b =>
                {
                    b.HasOne("LeilaoService.Leilao", "Leilao")
                        .WithOne("Item")
                        .HasForeignKey("LeilaoService.Item", "LeilaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leilao");
                });

            modelBuilder.Entity("LeilaoService.Leilao", b =>
                {
                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
