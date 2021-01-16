﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetCESI.Data.Context;

namespace ProjetCESI.Data.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20210116122028_ModifTableStatistique")]
    partial class ModifTableStatistique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjetCESI.Core.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ProjetCESI.Core.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProjetCESI.Core.Commentaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CommentaireParentId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreation")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateModification")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("RessourceId")
                        .HasColumnType("int");

                    b.Property<string>("Texte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentaireParentId");

                    b.HasIndex("RessourceId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Commentaires");
                });

            modelBuilder.Entity("ProjetCESI.Core.Ressource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreation")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateModification")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("EstValide")
                        .HasColumnType("bit");

                    b.Property<int>("NombreConsultation")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeRessourceId")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurCreateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.HasIndex("TypeRessourceId");

                    b.HasIndex("UtilisateurCreateurId");

                    b.ToTable("Ressources");
                });

            modelBuilder.Entity("ProjetCESI.Core.Statistique", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateRecherche")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Parametre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Statistiques");
                });

            modelBuilder.Entity("ProjetCESI.Core.TypeRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeRelations");
                });

            modelBuilder.Entity("ProjetCESI.Core.TypeRelationRessource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RessourceId")
                        .HasColumnType("int");

                    b.Property<int>("TypeRelationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RessourceId");

                    b.HasIndex("TypeRelationId");

                    b.ToTable("TypeRelationRessources");
                });

            modelBuilder.Entity("ProjetCESI.Core.TypeRessource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeRessources");
                });

            modelBuilder.Entity("ProjetCESI.Core.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

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

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ProjetCESI.Core.UtilisateurRessource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("EstExploite")
                        .HasColumnType("bit");

                    b.Property<bool>("EstFavoris")
                        .HasColumnType("bit");

                    b.Property<bool>("EstMisDeCote")
                        .HasColumnType("bit");

                    b.Property<int>("RessourceId")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RessourceId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("UtilisateurRessources");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("ProjetCESI.Core.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ProjetCESI.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ProjetCESI.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("ProjetCESI.Core.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ProjetCESI.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetCESI.Core.Commentaire", b =>
                {
                    b.HasOne("ProjetCESI.Core.Commentaire", "CommentaireParent")
                        .WithMany("CommentairesEnfant")
                        .HasForeignKey("CommentaireParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ProjetCESI.Core.Ressource", "Ressource")
                        .WithMany("Commentaires")
                        .HasForeignKey("RessourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.User", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentaireParent");

                    b.Navigation("Ressource");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("ProjetCESI.Core.Ressource", b =>
                {
                    b.HasOne("ProjetCESI.Core.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.TypeRessource", "TypeRessource")
                        .WithMany()
                        .HasForeignKey("TypeRessourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.User", "UtilisateurCreateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurCreateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("TypeRessource");

                    b.Navigation("UtilisateurCreateur");
                });

            modelBuilder.Entity("ProjetCESI.Core.Statistique", b =>
                {
                    b.HasOne("ProjetCESI.Core.User", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("ProjetCESI.Core.TypeRelationRessource", b =>
                {
                    b.HasOne("ProjetCESI.Core.Ressource", "Ressource")
                        .WithMany("TypeRelationsRessources")
                        .HasForeignKey("RessourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.TypeRelation", "TypeRelation")
                        .WithMany("TypeRelationsRessource")
                        .HasForeignKey("TypeRelationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ressource");

                    b.Navigation("TypeRelation");
                });

            modelBuilder.Entity("ProjetCESI.Core.UtilisateurRessource", b =>
                {
                    b.HasOne("ProjetCESI.Core.Ressource", "Ressource")
                        .WithMany("UtilisateurRessources")
                        .HasForeignKey("RessourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjetCESI.Core.User", "Utilisateur")
                        .WithMany("UtilisateurRessources")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ressource");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("ProjetCESI.Core.Commentaire", b =>
                {
                    b.Navigation("CommentairesEnfant");
                });

            modelBuilder.Entity("ProjetCESI.Core.Ressource", b =>
                {
                    b.Navigation("Commentaires");

                    b.Navigation("TypeRelationsRessources");

                    b.Navigation("UtilisateurRessources");
                });

            modelBuilder.Entity("ProjetCESI.Core.TypeRelation", b =>
                {
                    b.Navigation("TypeRelationsRessource");
                });

            modelBuilder.Entity("ProjetCESI.Core.User", b =>
                {
                    b.Navigation("UtilisateurRessources");
                });
#pragma warning restore 612, 618
        }
    }
}
