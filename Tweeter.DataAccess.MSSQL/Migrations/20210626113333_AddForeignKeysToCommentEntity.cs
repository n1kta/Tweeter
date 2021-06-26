using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweeter.DataAccess.MSSQL.Migrations
{
    public partial class AddForeignKeysToCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tweets_TweetId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TweetId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tweets_TweetId",
                table: "Comments",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tweets_TweetId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TweetId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tweets_TweetId",
                table: "Comments",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
