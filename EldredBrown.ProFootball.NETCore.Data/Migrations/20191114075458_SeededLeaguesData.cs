using Microsoft.EntityFrameworkCore.Migrations;

namespace EldredBrown.ProFootball.NETCore.Data.Migrations
{
    public partial class SeededLeaguesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_LongName",
                table: "Leagues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_LongName",
                table: "Leagues",
                column: "LongName");
        }
    }
}
