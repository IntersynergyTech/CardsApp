using Microsoft.EntityFrameworkCore.Migrations;

namespace CardsApp.Data.Migrations
{
    public partial class handscompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "PlayerHands",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "PlayerHands");
        }
    }
}
