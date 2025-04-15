﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tevo_service.Entities;

#nullable disable

namespace tevo_service.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("tevo_service.Entities.AddressInfo", b =>
                {
                    b.Property<long>("AddressInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("AddressInfoId"));

                    b.Property<string>("Latitude")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Longitude")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Type")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("VARCHAR(1000)");

                    b.HasKey("AddressInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("AddressInfo");
                });

            modelBuilder.Entity("tevo_service.Entities.Client", b =>
                {
                    b.Property<long>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ClientId"));

                    b.Property<string>("ClientAdres")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1000)");

                    b.Property<string>("ClientDeliverMilk")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ClientName")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("ClientPrice")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ClientRequestMilk")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("ClientSurname")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("ClientTelNo")
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("ClientId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("tevo_service.Entities.ContactInfo", b =>
                {
                    b.Property<long>("ContactInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ContactInfoId"));

                    b.Property<string>("Type")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("VARCHAR(500)");

                    b.HasKey("ContactInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("tevo_service.Entities.Demand", b =>
                {
                    b.Property<long>("DemandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("DemandId"));

                    b.Property<long?>("AddressInfoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ContactInfoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Currency")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp");

                    b.Property<decimal?>("Delivered")
                        .HasColumnType("numeric");

                    b.Property<Guid>("DelivererUserId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Demanded")
                        .HasColumnType("numeric");

                    b.Property<bool?>("ManuelMi")
                        .HasColumnType("boolean");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RecipientUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("DemandId");

                    b.HasIndex("DelivererUserId");

                    b.HasIndex("RecipientUserId");

                    b.ToTable("Demand");
                });

            modelBuilder.Entity("tevo_service.Entities.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ProductId"));

                    b.Property<string>("ProductName")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("Unit")
                        .HasColumnType("VARCHAR(200)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("tevo_service.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BanReason")
                        .HasColumnType("text");

                    b.Property<bool?>("IsBanned")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("VARCHAR(500)");

                    b.Property<string>("Role")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("UserName")
                        .HasColumnType("VARCHAR(200)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("tevo_service.Entities.AddressInfo", b =>
                {
                    b.HasOne("tevo_service.Entities.User", "User")
                        .WithMany("AddressInfoList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("tevo_service.Entities.ContactInfo", b =>
                {
                    b.HasOne("tevo_service.Entities.User", "User")
                        .WithMany("ContactInfoList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("tevo_service.Entities.Demand", b =>
                {
                    b.HasOne("tevo_service.Entities.User", "DelivererUser")
                        .WithMany("DeliveredDemands")
                        .HasForeignKey("DelivererUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("tevo_service.Entities.User", "RecipientUser")
                        .WithMany("ReceivedDemands")
                        .HasForeignKey("RecipientUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DelivererUser");

                    b.Navigation("RecipientUser");
                });

            modelBuilder.Entity("tevo_service.Entities.User", b =>
                {
                    b.Navigation("AddressInfoList");

                    b.Navigation("ContactInfoList");

                    b.Navigation("DeliveredDemands");

                    b.Navigation("ReceivedDemands");
                });
#pragma warning restore 612, 618
        }
    }
}
