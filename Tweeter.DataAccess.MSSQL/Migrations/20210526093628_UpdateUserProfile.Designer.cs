﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tweeter.DataAccess.MSSQL.Context;

namespace Tweeter.DataAccess.MSSQL.Migrations
{
    [DbContext(typeof(TweeterContext))]
    [Migration("20210526093628_UpdateUserProfile")]
    partial class UpdateUserProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TweetId")
                        .HasColumnType("int");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TweetId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.CommentLike", b =>
                {
                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.HasKey("UserProfileId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentLikes");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Follower", b =>
                {
                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.HasKey("FromUserId", "ToUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Follower");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Tweet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.TweetLike", b =>
                {
                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.Property<int>("TweetId")
                        .HasColumnType("int");

                    b.HasKey("UserProfileId", "TweetId");

                    b.HasIndex("TweetId");

                    b.ToTable("TweetLikes");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Comment", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.Tweet", "Tweet")
                        .WithMany("Comments")
                        .HasForeignKey("TweetId");

                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "UserProfile")
                        .WithMany("Comments")
                        .HasForeignKey("UserProfileId");

                    b.Navigation("Tweet");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.CommentLike", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.Comment", "Comment")
                        .WithMany("CommentLikes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "UserProfile")
                        .WithMany("CommentLikes")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Follower", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "FromUser")
                        .WithMany("Followings")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "ToUser")
                        .WithMany("Followers")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Tweet", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "UserProfile")
                        .WithMany("Tweets")
                        .HasForeignKey("UserProfileId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.TweetLike", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.Tweet", "Tweet")
                        .WithMany("TweetLikes")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "UserProfile")
                        .WithMany("TweetLikes")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tweet");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.UserProfile", b =>
                {
                    b.HasOne("Tweeter.DataAccess.MSSQL.Entities.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Tweeter.DataAccess.MSSQL.Entities.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Comment", b =>
                {
                    b.Navigation("CommentLikes");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.Tweet", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("TweetLikes");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.User", b =>
                {
                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Tweeter.DataAccess.MSSQL.Entities.UserProfile", b =>
                {
                    b.Navigation("CommentLikes");

                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("TweetLikes");

                    b.Navigation("Tweets");
                });
#pragma warning restore 612, 618
        }
    }
}
