using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Challange.Migrations
{
    public partial class ChallengeUtils2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChallengerId",
                table: "ChallengeRequest",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChallengeOwnerId",
                table: "ChallengeRequest",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "WinnerId",
                table: "Challenge",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChallengerId",
                table: "Challenge",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChallengeOwnerId",
                table: "Challenge",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChallengerId",
                table: "ChallengeRequest",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeOwnerId",
                table: "ChallengeRequest",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Challenge",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChallengerId",
                table: "Challenge",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeOwnerId",
                table: "Challenge",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
