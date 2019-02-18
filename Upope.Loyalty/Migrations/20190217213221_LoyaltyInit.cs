using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Loyalty.Migrations
{
    public partial class LoyaltyInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Point",
                columns: new[] { "Id", "Points", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 10, 1, "0fc04ca0-cc8d-4896-a50d-b9fe247cf152" },
                    { 2, 20, 1, "3282d1a0-ed0e-4f8a-8720-8f3474c21ecf" },
                    { 3, 30, 1, "ad687285-5202-4f81-8220-0a424dae3566" },
                    { 4, 40, 1, "897b2d91-6c1a-4bed-a4fc-9e84441e4b2d" },
                    { 5, 50, 1, "5961efe4-8ad8-488f-8727-022417b612a0" },
                    { 6, 60, 1, "d589a0cf-7172-4b44-bb49-609fcc7d56be" },
                    { 7, 70, 1, "e21b42d9-ca1f-49d5-b2f8-565a1bff3754" },
                    { 8, 80, 1, "b41b9a6b-00da-46dc-8115-67066009b211" },
                    { 9, 90, 1, "3eec55e4-3f38-4191-b43f-ba950108b296" },
                    { 10, 100, 1, "04dc0f14-65ce-4d93-8505-124d9f3b9276" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Point");
        }
    }
}
