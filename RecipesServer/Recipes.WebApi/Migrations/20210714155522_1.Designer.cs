﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Recipes.Infrastructure;

namespace Recipes.WebApi.Migrations
{
    [DbContext(typeof(RecipesContext))]
    [Migration("20210714155522_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Recipes.Domain.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CookingTimeMin")
                        .HasColumnType("integer")
                        .HasColumnName("cooking_time_min");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text")
                        .HasColumnName("image_path");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Portions")
                        .HasColumnType("integer")
                        .HasColumnName("portions");

                    b.Property<string[]>("Steps")
                        .HasColumnType("text[]")
                        .HasColumnName("steps");

                    b.HasKey("Id")
                        .HasName("pk_recipe");

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Recipe", b =>
                {
                    b.OwnsMany("Recipes.Domain.Models.RecipeIngredientBlock", "IngredientBlocks", b1 =>
                        {
                            b1.Property<int>("RecipeId")
                                .HasColumnType("integer")
                                .HasColumnName("recipe_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Header")
                                .HasColumnType("text")
                                .HasColumnName("header");

                            b1.Property<int>("Position")
                                .HasColumnType("integer")
                                .HasColumnName("position");

                            b1.Property<string>("Text")
                                .HasColumnType("text")
                                .HasColumnName("text");

                            b1.HasKey("RecipeId", "Id")
                                .HasName("pk_recipe_ingredient_block");

                            b1.ToTable("recipe_ingredient_block");

                            b1.WithOwner()
                                .HasForeignKey("RecipeId")
                                .HasConstraintName("fk_recipe_ingredient_block_recipe_recipe_id");
                        });

                    b.Navigation("IngredientBlocks");
                });
#pragma warning restore 612, 618
        }
    }
}
