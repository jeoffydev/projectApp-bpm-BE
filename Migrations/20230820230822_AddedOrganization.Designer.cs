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
    [Migration("20230820230822_AddedOrganization")]
    partial class AddedOrganization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                            PasswordHash = new byte[] { 195, 77, 50, 29, 96, 172, 99, 85, 6, 254, 74, 178, 123, 227, 131, 228, 4, 75, 120, 7, 23, 213, 171, 214, 119, 98, 10, 183, 22, 165, 71, 179, 5, 49, 233, 18, 67, 226, 242, 181, 139, 55, 54, 170, 78, 68, 124, 18, 110, 63, 95, 233, 13, 196, 57, 218, 204, 53, 61, 64, 147, 92, 81, 252 },
                            PasswordSalt = new byte[] { 205, 199, 164, 97, 73, 197, 39, 141, 27, 20, 72, 162, 212, 31, 90, 151, 112, 38, 201, 118, 33, 14, 1, 93, 193, 84, 19, 240, 43, 216, 240, 3, 103, 3, 74, 201, 36, 94, 203, 125, 113, 129, 132, 253, 97, 205, 73, 195, 61, 172, 152, 16, 189, 12, 242, 230, 134, 209, 99, 250, 194, 218, 40, 57, 33, 254, 36, 220, 105, 53, 103, 13, 214, 19, 248, 234, 126, 104, 242, 57, 75, 62, 124, 162, 113, 212, 126, 121, 116, 243, 116, 86, 86, 200, 172, 218, 132, 181, 173, 88, 35, 207, 17, 42, 149, 233, 162, 134, 174, 140, 15, 152, 211, 59, 170, 49, 117, 63, 119, 23, 127, 32, 154, 224, 242, 2, 32, 144 },
                            SecretKey = ""
                        });
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
