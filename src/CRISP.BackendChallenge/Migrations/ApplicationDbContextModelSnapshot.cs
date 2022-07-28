﻿// <auto-generated />
using System;
using CRISP.BackendChallenge.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRISP.BackendChallenge.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("CRISP.Backend.Challenge.Context.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LoginDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Logins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            LoginDate = new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9477)
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 1,
                            LoginDate = new DateTime(2022, 5, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9514)
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = 1,
                            LoginDate = new DateTime(2022, 4, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9516)
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = 2,
                            LoginDate = new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9519)
                        },
                        new
                        {
                            Id = 5,
                            EmployeeId = 2,
                            LoginDate = new DateTime(2022, 5, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9521)
                        },
                        new
                        {
                            Id = 6,
                            EmployeeId = 3,
                            LoginDate = new DateTime(2022, 6, 27, 11, 50, 9, 891, DateTimeKind.Local).AddTicks(9523)
                        });
                });

            modelBuilder.Entity("CRISP.BackendChallenge.Context.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Department")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Department = 1,
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Department = 2,
                            Name = "Jane Doe"
                        },
                        new
                        {
                            Id = 3,
                            Department = 1,
                            Name = "Joe Doe"
                        });
                });

            modelBuilder.Entity("CRISP.Backend.Challenge.Context.Models.Login", b =>
                {
                    b.HasOne("CRISP.BackendChallenge.Context.Models.Employee", null)
                        .WithMany("Logins")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CRISP.BackendChallenge.Context.Models.Employee", b =>
                {
                    b.Navigation("Logins");
                });
#pragma warning restore 612, 618
        }
    }
}