using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accepted_Assignment.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    MatchDate = table.Column<DateTime>(nullable: false),
                    TeamA = table.Column<string>(nullable: true),
                    TeamB = table.Column<string>(nullable: true),
                    Sport = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(nullable: false),
                    Specifier = table.Column<string>(nullable: true),
                    Odd = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId",
                table: "MatchOdds",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
