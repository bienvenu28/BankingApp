﻿// <auto-generated />
using BankingApp.Logic.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingApp.Migrations
{
    [DbContext(typeof(ClienContext))]
    [Migration("20211112161205_Add_Several_Default_Users")]
    partial class Add_Several_Default_Users
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("BankingApp.Logic.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("REAL")
                        .HasDefaultValue(0.0)
                        .HasColumnName("amount");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("firstName");

                    b.Property<bool>("IsBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("isBlocked");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("lastName");

                    b.Property<string>("Pin")
                        .HasColumnType("TEXT")
                        .HasColumnName("pin");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 200.19999999999999,
                            FirstName = "John",
                            IsBlocked = false,
                            LastName = "Dylan",
                            Pin = "12340"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 200000.20000000001,
                            FirstName = "Remi",
                            IsBlocked = false,
                            LastName = "Gold",
                            Pin = "4560"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 100000.0,
                            FirstName = "Bobby",
                            IsBlocked = false,
                            LastName = "Haang",
                            Pin = "7777"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
