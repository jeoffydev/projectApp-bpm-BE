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
    [Migration("20230814010444_AddOwnerSeed")]
    partial class AddOwnerSeed
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
                            PasswordHash = new byte[] { 149, 88, 216, 29, 191, 42, 26, 16, 215, 57, 14, 245, 248, 157, 66, 131, 179, 12, 201, 148, 36, 170, 208, 246, 89, 79, 134, 136, 107, 1, 9, 124, 64, 254, 44, 52, 210, 105, 81, 181, 9, 86, 159, 160, 34, 38, 200, 203, 80, 84, 87, 117, 206, 232, 106, 131, 192, 147, 192, 157, 27, 87, 82, 175 },
                            PasswordSalt = new byte[] { 17, 88, 197, 108, 219, 3, 190, 174, 93, 220, 218, 118, 170, 243, 250, 197, 191, 13, 94, 129, 152, 234, 151, 96, 128, 150, 166, 163, 219, 230, 255, 153, 250, 214, 6, 242, 175, 70, 98, 16, 166, 152, 112, 40, 149, 143, 243, 39, 118, 116, 225, 250, 2, 19, 166, 214, 11, 85, 116, 94, 137, 86, 124, 54, 136, 223, 52, 160, 220, 117, 154, 242, 184, 75, 169, 65, 17, 231, 178, 66, 106, 80, 91, 88, 76, 46, 78, 94, 162, 57, 132, 197, 137, 104, 52, 209, 47, 63, 235, 179, 67, 225, 250, 151, 251, 81, 156, 157, 220, 78, 141, 6, 45, 226, 170, 200, 189, 59, 55, 217, 92, 34, 154, 249, 6, 50, 81, 253 },
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
