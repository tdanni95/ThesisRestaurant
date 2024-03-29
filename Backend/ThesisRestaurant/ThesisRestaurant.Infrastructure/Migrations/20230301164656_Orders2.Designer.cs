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
    [Migration("20230301164656_Orders2")]
    partial class Orders2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CustomFoodIngredient", b =>
                {
                    b.Property<int>("CustomFoodsId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.HasKey("CustomFoodsId", "IngredientsId");

                    b.HasIndex("IngredientsId");

                    b.ToTable("CustomFoodIngredient");
                });

            modelBuilder.Entity("FoodIngredient", b =>
                {
                    b.Property<int>("FoodsId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.HasKey("FoodsId", "IngredientsId");

                    b.HasIndex("IngredientsId");

                    b.ToTable("FoodIngredient");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.CustomFoods.CustomFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FoodTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("CustomFoods");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.FoodTypes.FoodSizes.FoodSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FoodTypeId")
                        .HasColumnType("int");

                    b.Property<double>("Multiplier")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FoodTypeId");

                    b.ToTable("FoodSizes");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.FoodTypes.FoodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("FoodTypes");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<byte>("Visible")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.FoodPictures.FoodPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FoodId")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("FoodPictures");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.FoodPrices.FoodPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FoodId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("From")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<DateTime?>("To")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("FoodPrices");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Ingredients.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IngredientTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

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
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("IngredientTypes");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserAddressId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.OrderCustomItems.OrderCustomItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustumFoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<uint>("Quantity")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("CustumFoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderCustomItems");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.OrderItems.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<uint>("Quantity")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuthToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<byte>("Level")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Users.UserAddresses.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("CustomFoodIngredient", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.CustomFoods.CustomFood", null)
                        .WithMany()
                        .HasForeignKey("CustomFoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisRestaurant.Domain.Ingredients.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodIngredient", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisRestaurant.Domain.Ingredients.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.CustomFoods.CustomFood", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.FoodTypes.FoodType", "FoodType")
                        .WithMany()
                        .HasForeignKey("FoodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisRestaurant.Domain.Users.User", null)
                        .WithMany("CustomFoods")
                        .HasForeignKey("UserId");

                    b.Navigation("FoodType");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.FoodTypes.FoodSizes.FoodSize", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.FoodTypes.FoodType", "FoodType")
                        .WithMany()
                        .HasForeignKey("FoodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodType");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.FoodTypes.FoodType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.FoodPictures.FoodPicture", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.Food", null)
                        .WithMany("FoodPictures")
                        .HasForeignKey("FoodId");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.FoodPrices.FoodPrice", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.Food", null)
                        .WithMany("FoodPrices")
                        .HasForeignKey("FoodId");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Ingredients.Ingredient", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Ingredients.IngredientTypes.IngredientType", "IngredientType")
                        .WithMany()
                        .HasForeignKey("IngredientTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientType");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.Order", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Users.UserAddresses.UserAddress", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserAddressId");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.OrderCustomItems.OrderCustomItem", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.CustomFoods.CustomFood", "CustumFood")
                        .WithMany()
                        .HasForeignKey("CustumFoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisRestaurant.Domain.Orders.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustumFood");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Orders.OrderItems.OrderItem", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Foods.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisRestaurant.Domain.Orders.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Users.UserAddresses.UserAddress", b =>
                {
                    b.HasOne("ThesisRestaurant.Domain.Users.User", null)
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Foods.Food", b =>
                {
                    b.Navigation("FoodPictures");

                    b.Navigation("FoodPrices");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Users.User", b =>
                {
                    b.Navigation("CustomFoods");

                    b.Navigation("UserAddresses");
                });

            modelBuilder.Entity("ThesisRestaurant.Domain.Users.UserAddresses.UserAddress", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
