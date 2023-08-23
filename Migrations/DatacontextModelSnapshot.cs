﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp_bpm_core7_BE.Data;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    [DbContext(typeof(Datacontext))]
    partial class DatacontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasIndex("OrganizationId");

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
                            PasswordHash = new byte[] { 191, 73, 173, 144, 98, 178, 239, 81, 94, 192, 49, 171, 132, 13, 137, 39, 99, 32, 235, 206, 12, 190, 211, 4, 90, 188, 82, 194, 55, 210, 4, 215, 154, 95, 240, 158, 182, 214, 117, 109, 172, 249, 252, 71, 170, 1, 148, 230, 149, 100, 89, 255, 196, 114, 210, 117, 182, 184, 93, 158, 177, 213, 247, 232 },
                            PasswordSalt = new byte[] { 72, 40, 156, 211, 252, 102, 40, 89, 103, 97, 107, 71, 2, 204, 180, 188, 176, 67, 72, 10, 169, 141, 138, 7, 91, 188, 247, 251, 154, 83, 158, 18, 118, 40, 81, 5, 207, 245, 75, 150, 184, 12, 58, 55, 163, 0, 244, 158, 191, 201, 112, 151, 169, 140, 167, 85, 205, 37, 176, 219, 113, 250, 57, 134, 44, 145, 134, 211, 215, 198, 123, 73, 73, 50, 15, 42, 144, 207, 156, 222, 126, 171, 165, 150, 3, 205, 211, 34, 54, 53, 141, 28, 54, 189, 7, 97, 9, 191, 216, 59, 213, 44, 24, 111, 161, 142, 55, 219, 225, 165, 171, 131, 72, 172, 208, 107, 215, 65, 128, 195, 142, 64, 76, 67, 255, 28, 111, 132 },
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

                    b.HasOne("asp_bpm_core7_BE.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthRole");

                    b.Navigation("Organization");
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
