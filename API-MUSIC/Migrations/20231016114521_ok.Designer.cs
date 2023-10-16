﻿// <auto-generated />
using API_MUSIC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_MUSIC.Migrations
{
    [DbContext(typeof(IMusicContext))]
    [Migration("20231016114521_ok")]
    partial class ok
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Address", b =>
                {
                    b.Property<int>("IdAddress")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.HasKey("IdAddress");

                    b.ToTable("Addresse_Artists");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Artist", b =>
                {
                    b.Property<int>("IdArtist")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StyleMusic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Yearofbirth")
                        .HasColumnType("int");

                    b.HasKey("IdArtist");

                    b.HasIndex("AddressID")
                        .IsUnique();

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<string>("Compositor")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)");

                    b.Property<double>("Duration")
                        .HasColumnType("double");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistID");

                    b.ToTable("Music");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Artist", b =>
                {
                    b.HasOne("API_MUSIC.Controllers.Models.Address", "Address")
                        .WithOne("Artist")
                        .HasForeignKey("API_MUSIC.Controllers.Models.Artist", "AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Music", b =>
                {
                    b.HasOne("API_MUSIC.Controllers.Models.Artist", "Artist")
                        .WithMany("Musics")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Address", b =>
                {
                    b.Navigation("Artist");
                });

            modelBuilder.Entity("API_MUSIC.Controllers.Models.Artist", b =>
                {
                    b.Navigation("Musics");
                });
#pragma warning restore 612, 618
        }
    }
}