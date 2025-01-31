﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _3DTrackingProducts.Api.Data;

#nullable disable

namespace _3DTrackingProducts.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230327230331_AddRoomIdOnControlTagTable")]
    partial class AddRoomIdOnControlTagTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                            RoleId = new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Auth.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Auth.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c4b58f8f-6c40-411b-8a90-0e6b47c03ed9",
                            Email = "admin@email.pt",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EMAIL.PT",
                            PasswordHash = "AQAAAAIAAYagAAAAEK4Z2IVexGHzXaipmCxbM2DLWld1rCwQycJMDrEBZUg+Q+JF/oH2GCYIgo5T13s3eQ==",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "admin@email.pt"
                        });
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.ControlTag", b =>
                {
                    b.Property<string>("EPC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("PositionX")
                        .HasColumnType("float");

                    b.Property<double>("PositionY")
                        .HasColumnType("float");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EPC");

                    b.HasIndex("RoomId");

                    b.ToTable("ControlTags");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Angle")
                        .HasColumnType("float");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RSSI")
                        .HasColumnType("int");

                    b.Property<string>("TagEPC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TagEPC");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.PairAntenna", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("antenna01IP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("antenna01X")
                        .HasColumnType("int");

                    b.Property<int>("antenna01Y")
                        .HasColumnType("int");

                    b.Property<string>("antenna02IP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("antenna02X")
                        .HasColumnType("int");

                    b.Property<int>("antenna02Y")
                        .HasColumnType("int");

                    b.Property<Guid>("idRoom")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("idRoom");

                    b.ToTable("PairAntennas");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Room", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.Property<byte[]>("imageByte")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Tag", b =>
                {
                    b.Property<string>("EPC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EPC");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Tag3DPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ControlTagEPCLeft")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ControlTagEPCRight")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTimeRegistered")
                        .HasColumnType("datetime2");

                    b.Property<double>("DistanceLeft")
                        .HasColumnType("float");

                    b.Property<double>("DistanceRight")
                        .HasColumnType("float");

                    b.Property<double>("RelativePosX")
                        .HasColumnType("float");

                    b.Property<double>("RelativePosY")
                        .HasColumnType("float");

                    b.Property<double>("RelativePosZ")
                        .HasColumnType("float");

                    b.Property<string>("TagEPC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ControlTagEPCLeft");

                    b.HasIndex("ControlTagEPCRight");

                    b.HasIndex("TagEPC");

                    b.ToTable("Tag3DPositions");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.TagPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PairAntennaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagEPC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<double>("x")
                        .HasColumnType("float");

                    b.Property<double>("y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PairAntennaId");

                    b.HasIndex("TagEPC");

                    b.ToTable("TagPositions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.ControlTag", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Log", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Tag", "Tag")
                        .WithMany("Logs")
                        .HasForeignKey("TagEPC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.PairAntenna", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Room", "Room")
                        .WithMany("antennas")
                        .HasForeignKey("idRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Tag", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.Category", "Category")
                        .WithMany("Tags")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Tag3DPosition", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.ControlTag", "ControlTagLeft")
                        .WithMany("Tag3DPositionsLeft")
                        .HasForeignKey("ControlTagEPCLeft")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_3DTrackingProducts.Api.Models.ControlTag", "ControlTagRight")
                        .WithMany("Tag3DPositionsRight")
                        .HasForeignKey("ControlTagEPCRight")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_3DTrackingProducts.Api.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagEPC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ControlTagLeft");

                    b.Navigation("ControlTagRight");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.TagPosition", b =>
                {
                    b.HasOne("_3DTrackingProducts.Api.Models.PairAntenna", "PairAntenna")
                        .WithMany()
                        .HasForeignKey("PairAntennaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_3DTrackingProducts.Api.Models.Tag", "Tag")
                        .WithMany("TagPositions")
                        .HasForeignKey("TagEPC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PairAntenna");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Category", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.ControlTag", b =>
                {
                    b.Navigation("Tag3DPositionsLeft");

                    b.Navigation("Tag3DPositionsRight");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Room", b =>
                {
                    b.Navigation("antennas");
                });

            modelBuilder.Entity("_3DTrackingProducts.Api.Models.Tag", b =>
                {
                    b.Navigation("Logs");

                    b.Navigation("TagPositions");
                });
#pragma warning restore 612, 618
        }
    }
}
