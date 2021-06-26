using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweeter.DataAccess.MSSQL.Migrations
{
    public partial class AddIsEveryoneModeForTweetEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEveryoneMode",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEveryoneMode",
                table: "Tweets");
        }
    }
}
