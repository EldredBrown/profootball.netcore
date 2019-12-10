using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EldredBrown.ProFootball.NETCore.Data.Migrations
{
    public partial class AddedAndSeededAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    LeagueName = table.Column<string>(nullable: true),
                    FirstSeasonId = table.Column<int>(nullable: false),
                    LastSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LeagueName = table.Column<string>(nullable: true),
                    ConferenceName = table.Column<string>(nullable: true),
                    FirstSeasonId = table.Column<int>(nullable: false),
                    LastSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonId = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    GuestName = table.Column<string>(nullable: false),
                    GuestScore = table.Column<int>(nullable: false),
                    HostName = table.Column<string>(nullable: false),
                    HostScore = table.Column<int>(nullable: false),
                    WinnerName = table.Column<string>(nullable: true),
                    WinnerScore = table.Column<int>(nullable: true),
                    LoserName = table.Column<string>(nullable: true),
                    LoserScore = table.Column<int>(nullable: true),
                    IsPlayoffGame = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeagueSeasons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueName = table.Column<string>(nullable: true),
                    SeasonId = table.Column<int>(nullable: false),
                    TotalGames = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<int>(nullable: false),
                    AveragePoints = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueSeasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfWeeks = table.Column<int>(nullable: false),
                    NumOfWeeksCompleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeamSeasons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: true),
                    SeasonId = table.Column<int>(nullable: false),
                    LeagueName = table.Column<string>(nullable: true),
                    ConferenceName = table.Column<string>(nullable: true),
                    DivisionName = table.Column<string>(nullable: true),
                    Games = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Ties = table.Column<int>(nullable: false),
                    WinningPercentage = table.Column<decimal>(nullable: true),
                    PointsFor = table.Column<int>(nullable: false),
                    PointsAgainst = table.Column<int>(nullable: false),
                    PythagoreanWins = table.Column<decimal>(nullable: false),
                    PythagoreanLosses = table.Column<decimal>(nullable: false),
                    OffensiveAverage = table.Column<decimal>(nullable: true),
                    OffensiveFactor = table.Column<decimal>(nullable: true),
                    OffensiveIndex = table.Column<decimal>(nullable: true),
                    DefensiveAverage = table.Column<decimal>(nullable: true),
                    DefensiveFactor = table.Column<decimal>(nullable: true),
                    DefensiveIndex = table.Column<decimal>(nullable: true),
                    FinalPythagoreanWinningPercentage = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSeasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "ID", "GuestName", "GuestScore", "HostName", "HostScore", "IsPlayoffGame", "LoserName", "LoserScore", "Notes", "SeasonId", "Week", "WinnerName", "WinnerScore" },
                values: new object[,]
                {
                    { 1, "St. Paul Ideals", 0, "Rock Island Independents", 48, false, null, null, null, 1920, 1, null, null },
                    { 2, "Pitcairn Quakers", 0, "Canton Bulldogs", 48, false, null, null, null, 1920, 2, null, null },
                    { 3, "West Buffalo", 6, "Buffalo All-Americans", 32, false, null, null, null, 1920, 2, null, null },
                    { 4, "Wheeling Stogies", 0, "Akron Pros", 43, false, null, null, null, 1920, 2, null, null },
                    { 5, "Muncie Flyers", 0, "Rock Island Independents", 45, false, null, null, null, 1920, 2, null, null },
                    { 6, "All Buffalo", 0, "Rochester Jeffersons", 10, false, null, null, null, 1920, 2, null, null },
                    { 7, "Columbus Panhandles", 0, "Dayton Triangles", 14, false, null, null, null, 1920, 2, null, null },
                    { 8, "Moline Universal Tractors", 0, "Decatur Staleys", 20, false, null, null, null, 1920, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "LeagueSeasons",
                columns: new[] { "ID", "AveragePoints", "LeagueName", "SeasonId", "TotalGames", "TotalPoints" },
                values: new object[,]
                {
                    { 2, null, "American Professional Football Association", 1921, 0, 0 },
                    { 1, null, "American Professional Football Association", 1920, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "ID", "NumOfWeeks", "NumOfWeeksCompleted" },
                values: new object[,]
                {
                    { 1920, 13, 0 },
                    { 1921, 13, 0 }
                });

            migrationBuilder.InsertData(
                table: "TeamSeasons",
                columns: new[] { "ID", "ConferenceName", "DefensiveAverage", "DefensiveFactor", "DefensiveIndex", "DivisionName", "FinalPythagoreanWinningPercentage", "Games", "LeagueName", "Losses", "OffensiveAverage", "OffensiveFactor", "OffensiveIndex", "PointsAgainst", "PointsFor", "PythagoreanLosses", "PythagoreanWins", "SeasonId", "TeamName", "Ties", "WinningPercentage", "Wins" },
                values: new object[,]
                {
                    { 17, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Canton Bulldogs", 0, null, 0 },
                    { 16, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Buffalo All-Americans", 0, null, 0 },
                    { 15, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Akron Pros", 0, null, 0 },
                    { 14, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Rock Island Independents", 0, null, 0 },
                    { 13, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Rochester Jeffersons", 0, null, 0 },
                    { 7, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Columbus Panhandles", 0, null, 0 },
                    { 11, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Hammond Pros", 0, null, 0 },
                    { 10, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Detroit Heralds", 0, null, 0 },
                    { 9, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Decatur Staleys", 0, null, 0 },
                    { 8, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Dayton Triangles", 0, null, 0 },
                    { 18, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Chicago Cardinals", 0, null, 0 },
                    { 12, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Muncie Flyers", 0, null, 0 },
                    { 19, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Chicago Staleys", 0, null, 0 },
                    { 25, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Evansville Crimson Giants", 0, null, 0 },
                    { 21, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Cleveland Indians", 0, null, 0 },
                    { 22, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Columbus Panhandles", 0, null, 0 },
                    { 23, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Dayton Triangles", 0, null, 0 },
                    { 24, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Detroit Tigers", 0, null, 0 },
                    { 6, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Cleveland Tigers", 0, null, 0 },
                    { 26, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Green Bay Packers", 0, null, 0 },
                    { 27, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Hammond Pros", 0, null, 0 },
                    { 28, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Louisville Brocks", 0, null, 0 },
                    { 29, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Minneapolis Marines", 0, null, 0 },
                    { 30, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Muncie Flyers", 0, null, 0 },
                    { 31, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "New York Brickley Giants", 0, null, 0 },
                    { 32, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Rochester Jeffersons", 0, null, 0 },
                    { 33, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Rock Island Independents", 0, null, 0 },
                    { 20, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Cincinnati Celts", 0, null, 0 },
                    { 5, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Chicago Tigers", 0, null, 0 },
                    { 35, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Washington Senators", 0, null, 0 },
                    { 3, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Canton Bulldogs", 0, null, 0 },
                    { 2, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Buffalo All-Americans", 0, null, 0 },
                    { 1, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Akron Pros", 0, null, 0 },
                    { 34, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1921, "Tonawanda Kardex", 0, null, 0 },
                    { 4, null, null, null, null, null, null, 0, "American Professional Football Association", 0, null, null, null, 0, 0, 0m, 0m, 1920, "Chicago Cardinals", 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Akron Pros" },
                    { 2, "Buffalo All-Americans" },
                    { 3, "Canton Bulldogs" },
                    { 4, "Chicago Cardinals" },
                    { 5, "Chicago Tigers" },
                    { 6, "Cleveland Tigers" },
                    { 7, "Columbus Panhandles" },
                    { 8, "Dayton Triangles" },
                    { 9, "Decatur Staleys" },
                    { 10, "Detroit Heralds" },
                    { 11, "Hammond Pros" },
                    { 14, "Rock Island Independents" },
                    { 13, "Rochester Jeffersons" },
                    { 15, "Chicago Staleys" },
                    { 16, "Cincinnati Celts" },
                    { 17, "Cleveland Indians" },
                    { 18, "Detroit Tigers" },
                    { 19, "Evansville Crimson Giants" },
                    { 20, "Green Bay Packers" },
                    { 21, "Louisville Brocks" },
                    { 22, "Minneapolis Marines" },
                    { 23, "New York Brickley Giants" },
                    { 25, "Washington Senators" },
                    { 12, "Muncie Flyers" },
                    { 24, "Tonawanda Kardex" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "LeagueSeasons");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TeamSeasons");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "ID", "FirstSeasonId", "LastSeasonId", "LongName", "ShortName" },
                values: new object[] { 2, 1922, null, "National Football League", "NFL" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "ID", "FirstSeasonId", "LastSeasonId", "LongName", "ShortName" },
                values: new object[] { 3, 1946, 1949, "All-America Football Conference", "AAFC" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "ID", "FirstSeasonId", "LastSeasonId", "LongName", "ShortName" },
                values: new object[] { 4, 1960, 1969, "American Football League", "AFL" });
        }
    }
}
