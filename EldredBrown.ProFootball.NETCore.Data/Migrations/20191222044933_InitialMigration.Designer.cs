﻿// <auto-generated />
using System;
using EldredBrown.ProFootball.NETCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EldredBrown.ProFootball.NETCore.Data.Migrations
{
    [DbContext(typeof(ProFootballDbContext))]
    [Migration("20191222044933_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.Conference", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FirstSeasonYear")
                        .HasColumnType("int");

                    b.Property<int?>("LastSeasonYear")
                        .HasColumnType("int");

                    b.Property<string>("LeagueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.Division", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConferenceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FirstSeasonYear")
                        .HasColumnType("int");

                    b.Property<int?>("LastSeasonYear")
                        .HasColumnType("int");

                    b.Property<string>("LeagueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuestScore")
                        .HasColumnType("int");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HostScore")
                        .HasColumnType("int");

                    b.Property<bool>("IsPlayoffGame")
                        .HasColumnType("bit");

                    b.Property<string>("LoserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoserScore")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeasonYear")
                        .HasColumnType("int");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.Property<string>("WinnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WinnerScore")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.League", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FirstSeasonYear")
                        .HasColumnType("int");

                    b.Property<int?>("LastSeasonYear")
                        .HasColumnType("int");

                    b.Property<string>("LongName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("ID");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.LeagueSeason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("AveragePoints")
                        .HasColumnType("float");

                    b.Property<string>("LeagueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeasonYear")
                        .HasColumnType("int");

                    b.Property<int>("TotalGames")
                        .HasColumnType("int");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("LeagueSeasons");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.Season", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumOfWeeksScheduled")
                        .HasColumnType("int");

                    b.Property<int>("NumOfWeeksCompleted")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.SeasonTeamStanding", b =>
                {
                    b.Property<double?>("AvgPointsAgainst")
                        .HasColumnType("float");

                    b.Property<double?>("AvgPointsFor")
                        .HasColumnType("float");

                    b.Property<string>("Conference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("PointsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("PointsFor")
                        .HasColumnType("int");

                    b.Property<string>("Team")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ties")
                        .HasColumnType("int");

                    b.Property<double?>("WinningPercentage")
                        .HasColumnType("float");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.ToTable("SeasonStandings");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.TeamSeason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConferenceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DefensiveAverage")
                        .HasColumnType("float");

                    b.Property<double?>("DefensiveFactor")
                        .HasColumnType("float");

                    b.Property<double?>("DefensiveIndex")
                        .HasColumnType("float");

                    b.Property<string>("DivisionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FinalPythagoreanWinningPercentage")
                        .HasColumnType("float");

                    b.Property<int>("Games")
                        .HasColumnType("int");

                    b.Property<string>("LeagueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<double?>("OffensiveAverage")
                        .HasColumnType("float");

                    b.Property<double?>("OffensiveFactor")
                        .HasColumnType("float");

                    b.Property<double?>("OffensiveIndex")
                        .HasColumnType("float");

                    b.Property<int>("PointsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("PointsFor")
                        .HasColumnType("int");

                    b.Property<double>("PythagoreanLosses")
                        .HasColumnType("float");

                    b.Property<double>("PythagoreanWins")
                        .HasColumnType("float");

                    b.Property<int>("SeasonYear")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ties")
                        .HasColumnType("int");

                    b.Property<double?>("WinningPercentage")
                        .HasColumnType("float");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TeamSeasons");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.TeamSeasonOpponentProfile", b =>
                {
                    b.Property<int?>("GamePointsAgainst")
                        .HasColumnType("int");

                    b.Property<int?>("GamePointsFor")
                        .HasColumnType("int");

                    b.Property<string>("Opponent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OpponentLosses")
                        .HasColumnType("int");

                    b.Property<int?>("OpponentTies")
                        .HasColumnType("int");

                    b.Property<double?>("OpponentWeightedGames")
                        .HasColumnType("float");

                    b.Property<double?>("OpponentWeightedPointsAgainst")
                        .HasColumnType("float");

                    b.Property<double?>("OpponentWeightedPointsFor")
                        .HasColumnType("float");

                    b.Property<double?>("OpponentWinningPercentage")
                        .HasColumnType("float");

                    b.Property<int?>("OpponentWins")
                        .HasColumnType("int");

                    b.ToTable("TeamSeasonScheduleProfile");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.TeamSeasonScheduleAverages", b =>
                {
                    b.Property<double?>("PointsAgainst")
                        .HasColumnType("float");

                    b.Property<double?>("PointsFor")
                        .HasColumnType("float");

                    b.Property<double?>("SchedulePointsAgainst")
                        .HasColumnType("float");

                    b.Property<double?>("SchedulePointsFor")
                        .HasColumnType("float");

                    b.ToTable("TeamSeasonScheduleAverages");
                });

            modelBuilder.Entity("EldredBrown.ProFootball.NETCore.Data.Entities.TeamSeasonScheduleTotals", b =>
                {
                    b.Property<int?>("Games")
                        .HasColumnType("int");

                    b.Property<int?>("PointsAgainst")
                        .HasColumnType("int");

                    b.Property<int?>("PointsFor")
                        .HasColumnType("int");

                    b.Property<double?>("ScheduleGames")
                        .HasColumnType("float");

                    b.Property<int?>("ScheduleLosses")
                        .HasColumnType("int");

                    b.Property<double?>("SchedulePointsAgainst")
                        .HasColumnType("float");

                    b.Property<double?>("SchedulePointsFor")
                        .HasColumnType("float");

                    b.Property<int?>("ScheduleTies")
                        .HasColumnType("int");

                    b.Property<double?>("ScheduleWinningPercentage")
                        .HasColumnType("float");

                    b.Property<int?>("ScheduleWins")
                        .HasColumnType("int");

                    b.ToTable("TeamSeasonScheduleTotals");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
