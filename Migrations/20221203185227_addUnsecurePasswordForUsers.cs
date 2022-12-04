using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmasLottery.Migrations
{
    public partial class addUnsecurePasswordForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserData");
        }
    }
}
