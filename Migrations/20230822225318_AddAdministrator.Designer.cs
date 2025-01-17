﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp_bpm_core7_BE.Data;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    [DbContext(typeof(Datacontext))]
    [Migration("20230822225318_AddAdministrator")]
    partial class AddAdministrator
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("asp_bpm_core7_BE.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AuthRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecretKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthRoleId");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("asp_bpm_core7_BE.Models.AuthRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuthRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleDescription = "The owner with highest permission on this app",
                            RoleName = "Owner"
                        },
                        new
                        {
                            Id = 2,
                            RoleDescription = "The Customer administrator and has high permission",
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            RoleDescription = "Limited access in the customers private pages",
                            RoleName = "Member"
                        },
                        new
                        {
                            Id = 4,
                            RoleDescription = "Static and read only access",
                            RoleName = "Contractor"
                        });
                });

            modelBuilder.Entity("asp_bpm_core7_BE.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("asp_bpm_core7_BE.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AuthRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SecretKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthRoleId");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1001,
                            Active = true,
                            AuthRoleId = 1,
                            Email = "jeoffy_hipolito@yahoo.com",
                            FullName = "Jeoffy Hipolito",
                            PasswordHash = new byte[] { 41, 49, 0, 23, 105, 39, 96, 211, 225, 177, 129, 237, 32, 162, 68, 227, 190, 17, 144, 195, 10, 122, 67, 171, 134, 19, 168, 39, 153, 71, 137, 57, 234, 240, 25, 106, 236, 104, 154, 111, 80, 122, 248, 194, 150, 105, 192, 254, 94, 17, 105, 198, 248, 9, 56, 61, 188, 206, 107, 56, 73, 143, 42, 138 },
                            PasswordSalt = new byte[] { 19, 243, 165, 181, 7, 235, 138, 228, 180, 167, 243, 50, 50, 243, 177, 22, 162, 25, 151, 28, 52, 208, 152, 119, 229, 102, 128, 130, 72, 67, 229, 10, 177, 160, 200, 119, 245, 210, 176, 121, 236, 207, 181, 102, 93, 69, 240, 193, 252, 78, 189, 196, 194, 123, 189, 131, 193, 64, 87, 106, 204, 0, 132, 107, 241, 125, 14, 145, 208, 244, 63, 69, 57, 32, 177, 120, 24, 159, 228, 72, 40, 189, 42, 183, 65, 239, 214, 79, 186, 118, 233, 239, 150, 227, 240, 187, 87, 189, 105, 177, 117, 71, 4, 2, 162, 248, 125, 219, 86, 252, 97, 113, 32, 109, 139, 108, 87, 244, 225, 177, 161, 2, 208, 248, 45, 170, 78, 140 },
                            SecretKey = ""
                        });
                });

            modelBuilder.Entity("asp_bpm_core7_BE.Models.Administrator", b =>
                {
                    b.HasOne("asp_bpm_core7_BE.Models.AuthRole", "AuthRole")
                        .WithMany()
                        .HasForeignKey("AuthRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthRole");
                });

            modelBuilder.Entity("asp_bpm_core7_BE.Models.Owner", b =>
                {
                    b.HasOne("asp_bpm_core7_BE.Models.AuthRole", "AuthRole")
                        .WithMany()
                        .HasForeignKey("AuthRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthRole");
                });
#pragma warning restore 612, 618
        }
    }
}
