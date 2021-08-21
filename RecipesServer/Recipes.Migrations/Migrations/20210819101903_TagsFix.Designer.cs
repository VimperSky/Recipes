﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Recipes.Infrastructure;

namespace Recipes.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    [Migration("20210819101903_TagsFix")]
    partial class TagsFix
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

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer")
                        .HasColumnName("author_id");

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
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Portions")
                        .HasColumnType("integer")
                        .HasColumnName("portions");

                    b.Property<string[]>("Steps")
                        .HasColumnType("text[]")
                        .HasColumnName("steps");

                    b.Property<int[]>("TagIds")
                        .HasColumnType("integer[]")
                        .HasColumnName("tag_ids");

                    b.HasKey("Id")
                        .HasName("pk_recipe");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_recipe_author_id");

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_id");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_tag");

                    b.HasIndex("RecipeId")
                        .HasDatabaseName("ix_tag_recipe_id");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("Recipes.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_salt");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasDatabaseName("ix_user_login");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Recipe", b =>
                {
                    b.HasOne("Recipes.Domain.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("fk_recipe_users_author_id");

                    b.OwnsMany("Recipes.Domain.Models.RecipeIngredientsBlock", "Ingredients", b1 =>
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

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("value");

                            b1.HasKey("RecipeId", "Id")
                                .HasName("pk_recipe_ingredient_block");

                            b1.ToTable("recipe_ingredient_block");

                            b1.WithOwner()
                                .HasForeignKey("RecipeId")
                                .HasConstraintName("fk_recipe_ingredient_block_recipe_recipe_id");
                        });

                    b.Navigation("Author");

                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Tag", b =>
                {
                    b.HasOne("Recipes.Domain.Models.Recipe", null)
                        .WithMany("Tags")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("fk_tag_recipes_recipe_id");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Recipe", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
