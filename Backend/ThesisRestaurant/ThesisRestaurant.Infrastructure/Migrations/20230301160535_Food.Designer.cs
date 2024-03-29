﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisRestaurant.Infrastructure.Persistence;

#nullable disable

namespace ThesisRestaurant.Infrastructure.Migrations
{
    [DbContext(typeof(ThesisRestaurantDbContext))]
    [Migration("20230301160535_Food")]
    partial class Food
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ThesisRestaurant.Domain.FoodTypes.FoodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FoodTypes");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FoodPictureId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<byte>("Visible")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("FoodPictureId");

                    b.HasIndex("TypeId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.FoodPictures.FoodPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FoodPictures");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Ingredients.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientTypeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Ingredients.IngredientTypes.IngredientType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("IngredientTypes");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.FoodPictures.FoodPicture", "FoodPicture")
                        .WithMany()
                        .HasForeignKey("FoodPictureId");

                    b.HasOne("ThesisRestaurant.Domain.FoodTypes.FoodType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodPicture");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Ingredients.Ingredient", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.Food", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodId");

                    b.HasOne("ThesisRestaurant.Domain.Ingredients.IngredientTypes.IngredientType", "IngredientType")
                        .WithMany()
                        .HasForeignKey("IngredientTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientType");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
