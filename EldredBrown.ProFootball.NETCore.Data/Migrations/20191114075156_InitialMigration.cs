using Microsoft.EntityFrameworkCore.Migrations;

namespace EldredBrown.ProFootball.NETCore.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongName = table.Column<string>(maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(maxLength: 5, nullable: false),
                    FirstSeasonId = table.Column<int>(nullable: false),
                    LastSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.ID);
                    table.UniqueConstraint("AlternateKey_LongName", x => x.LongName);
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "ID", "FirstSeasonId", "LastSeasonId", "LongName", "ShortName" },
                values: new object[,]
                {
                    { 1, 1920, 1921, "American Professional Football Association", "APFA" },
                    { 2, 1922, null, "National Football League", "NFL" },
                    { 3, 1946, 1949, "All-America Football Conference", "AAFC" },
                    { 4, 1960, 1969, "American Football League", "AFL" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
