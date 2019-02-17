using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Challange.Migrations
{
    public partial class ChallengeRequestRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChallengeOwnerId",
                table: "Challenge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChallengerId",
                table: "Challenge",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RewardPoint",
                table: "Challenge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Challenge",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChallengeRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false),
                    ChallengeOwnerId = table.Column<int>(nullable: false),
                    ChallengerId = table.Column<int>(nullable: true),
                    ChallengeRequestStatus = table.Column<int>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeRequest_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeRequest_ChallengeId",
                table: "ChallengeRequest",
                column: "ChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeRequest");

            migrationBuilder.DropColumn(
                name: "ChallengeOwnerId",
                table: "Challenge");

            migrationBuilder.DropColumn(
                name: "ChallengerId",
                table: "Challenge");

            migrationBuilder.DropColumn(
                name: "RewardPoint",
                table: "Challenge");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Challenge");
        }
    }
}
