﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Recipes.Infrastructure;

namespace Recipes.Infrastructure.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    partial class RecipesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipesId")
                        .HasColumnType("integer")
                        .HasColumnName("recipes_id");

                    b.Property<string>("TagsValue")
                        .HasColumnType("text")
                        .HasColumnName("tags_value");

                    b.HasKey("RecipesId", "TagsValue")
                        .HasName("pk_recipe_tag");

                    b.HasIndex("TagsValue")
                        .HasDatabaseName("ix_recipe_tag_tags_value");

                    b.ToTable("recipe_tag");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Activity", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_liked");

                    b.Property<bool>("IsStarred")
                        .HasColumnType("boolean")
                        .HasColumnName("is_starred");

                    b.HasKey("RecipeId", "UserId")
                        .HasName("pk_activities");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_activities_user_id");

                    b.ToTable("activities");
                });

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

                    b.HasKey("Id")
                        .HasName("pk_recipe");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_recipe_author_id");

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("Recipes.Domain.Models.Tag", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Icon")
                        .HasColumnType("text")
                        .HasColumnName("icon");

                    b.Property<int>("TagLevel")
                        .HasColumnType("integer")
                        .HasColumnName("tag_level");

                    b.HasKey("Value")
                        .HasName("pk_tag");

                    b.HasIndex("Value")
                        .IsUnique()
                        .HasDatabaseName("ix_tag_value");

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

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("Recipes.Domain.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .HasConstraintName("fk_recipe_tag_recipes_recipes_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.Domain.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsValue")
                        .HasConstraintName("fk_recipe_tag_tag_tags_value")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.Domain.Models.Activity", b =>
                {
                    b.HasOne("Recipes.Domain.Models.Recipe", null)
                        .WithMany("Activities")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("fk_activities_recipes_recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.Domain.Models.User", null)
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_activities_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Recipes.Domain.Models.Recipe", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("Recipes.Domain.Models.User", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
