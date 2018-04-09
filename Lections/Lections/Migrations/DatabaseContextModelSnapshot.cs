﻿// <auto-generated />
using Lections.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Lections.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lections.Models.Lection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<DateTime>("dateCreate");

                    b.Property<DateTime>("dateUpdate");

                    b.Property<string>("name");

                    b.Property<string>("smallDescription");

                    b.Property<double>("stars");

                    b.Property<string>("subject");

                    b.Property<string>("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Lections");
                });

            modelBuilder.Entity("Lections.Models.Likes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LectionId");

                    b.Property<int>("UserId");

                    b.Property<int>("userStar");

                    b.HasKey("Id");

                    b.HasIndex("LectionId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Lections.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ammountLections");

                    b.Property<int>("ammountStars");

                    b.Property<string>("email");

                    b.Property<bool>("emailConfirmed");

                    b.Property<string>("firstname");

                    b.Property<bool>("isAdmin");

                    b.Property<string>("lastname");

                    b.Property<string>("password");

                    b.Property<string>("username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lections.Models.Lection", b =>
                {
                    b.HasOne("Lections.Models.User", "User")
                        .WithMany("Lections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lections.Models.Likes", b =>
                {
                    b.HasOne("Lections.Models.Lection")
                        .WithMany("Likes")
                        .HasForeignKey("LectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lections.Models.User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
