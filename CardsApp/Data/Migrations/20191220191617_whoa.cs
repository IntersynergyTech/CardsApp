using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardsApp.Data.Migrations
{
    public partial class whoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DefaultStake = table.Column<decimal>(nullable: false),
                    SingleWinner = table.Column<bool>(nullable: false),
                    MaximumScore = table.Column<int>(nullable: false),
                    DrawPosition = table.Column<int>(nullable: false),
                    HandsPlayed = table.Column<int>(nullable: false),
                    HandsPlayedPerPlayer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamesPlayed",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    WinnerId = table.Column<Guid>(nullable: true),
                    GameComplete = table.Column<bool>(nullable: false),
                    IsDraw = table.Column<bool>(nullable: false),
                    DrawParentId = table.Column<Guid>(nullable: true),
                    Stake = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlayed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesPlayed_GamesPlayed_DrawParentId",
                        column: x => x.DrawParentId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesPlayed_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hands_GamesPlayed_GameId",
                        column: x => x.GameId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Initial = table.Column<string>(nullable: true),
                    LastPlayed = table.Column<DateTime>(nullable: false),
                    LastPaid = table.Column<DateTime>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false),
                    GamePlayedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_GamesPlayed_GamePlayedId",
                        column: x => x.GamePlayedId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    PlayerId = table.Column<Guid>(nullable: true),
                    Difference = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Board_GamesPlayed_GameId",
                        column: x => x.GameId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamesPlayedByPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: true),
                    GamePlayedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlayedByPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesPlayedByPlayers_GamesPlayed_GamePlayedId",
                        column: x => x.GamePlayedId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesPlayedByPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerHands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: true),
                    HandId = table.Column<Guid>(nullable: true),
                    KnockedOut = table.Column<bool>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerHands_Hands_HandId",
                        column: x => x.HandId,
                        principalTable: "Hands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerHands_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Board_GameId",
                table: "Board",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_PlayerId",
                table: "Board",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_DrawParentId",
                table: "GamesPlayed",
                column: "DrawParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_GameId",
                table: "GamesPlayed",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayed_WinnerId",
                table: "GamesPlayed",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayedByPlayers_GamePlayedId",
                table: "GamesPlayedByPlayers",
                column: "GamePlayedId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlayedByPlayers_PlayerId",
                table: "GamesPlayedByPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hands_GameId",
                table: "Hands",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_HandId",
                table: "PlayerHands",
                column: "HandId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_PlayerId",
                table: "PlayerHands",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GamePlayedId",
                table: "Players",
                column: "GamePlayedId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesPlayed_Players_WinnerId",
                table: "GamesPlayed",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GamesPlayed_GamePlayedId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "GamesPlayedByPlayers");

            migrationBuilder.DropTable(
                name: "PlayerHands");

            migrationBuilder.DropTable(
                name: "Hands");

            migrationBuilder.DropTable(
                name: "GamesPlayed");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
