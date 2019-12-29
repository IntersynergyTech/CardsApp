using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardsApp.Data.Migrations
{
    public partial class Gameplayedplayersfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GamesPlayed_GamePlayedId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GamePlayedId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GamePlayedId",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GamePlayedId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GamePlayedId",
                table: "Players",
                column: "GamePlayedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GamesPlayed_GamePlayedId",
                table: "Players",
                column: "GamePlayedId",
                principalTable: "GamesPlayed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
