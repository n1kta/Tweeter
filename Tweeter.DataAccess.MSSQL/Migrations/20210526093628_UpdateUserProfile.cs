using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweeter.DataAccess.MSSQL.Migrations
{
    public partial class UpdateUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Phone",
                table: "UserProfiles",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
