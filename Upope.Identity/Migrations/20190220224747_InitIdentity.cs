using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Identity.Migrations
{
    public partial class InitIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    Nickname = table.Column<string>(maxLength: 250, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    FacebookId = table.Column<string>(maxLength: 250, nullable: true),
                    GoogleId = table.Column<string>(maxLength: 250, nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11716157-ed82-43b2-8454-50b55623a15c", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "0ba1c110-5ee6-459f-acd1-2af3f2ebf55e", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA1@GMAIL.COM", "MURATCANTUNA1", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna1" },
                    { "c4d18448-6ac8-421d-9349-2249d6d9b32a", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "ab957668-dfa7-422e-ac5c-027104f96721", "muratcantuna2@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA2@GMAIL.COM", "MURATCANTUNA2", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna2" },
                    { "52e40b92-928e-4861-b84c-9ba41fc03bc6", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "6b714b77-d19b-4a53-9e67-182694f2f5be", "muratcantuna3@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA3@GMAIL.COM", "MURATCANTUNA3", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna3" },
                    { "430c9a05-d533-4ffa-a424-719702fef001", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "e7b4d62b-ee09-49ef-9b0a-0da907a19ef2", "muratcantuna4@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA4@GMAIL.COM", "MURATCANTUNA4", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna4" },
                    { "266dab5c-8361-4beb-94fa-431e9174f3f9", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "a51cbff7-1021-42d2-a766-971ea2cb442c", "muratcantuna5@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA5@GMAIL.COM", "MURATCANTUNA5", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna5" },
                    { "3232b60f-dd51-46e7-878b-b5568e1d08e6", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "228e6a4e-c050-41cc-91e8-62ce1fed859e", "muratcantuna6@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA6@GMAIL.COM", "MURATCANTUNA6", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna6" },
                    { "e5a7885c-97c7-4200-b87d-68f94c2caa69", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "4068088a-b302-489a-8a8b-981e36be7bee", "muratcantuna7@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA7@GMAIL.COM", "MURATCANTUNA7", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna7" },
                    { "d92cff90-357a-463f-961e-f980ff40ff91", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "730c90a7-cb7c-47d7-b5c5-d6064752e792", "muratcantuna8@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA8@GMAIL.COM", "MURATCANTUNA8", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna8" },
                    { "75b9d964-9c08-4ca5-b220-c09bfc9e8a30", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "28977ff2-e3e4-4dd6-95c6-9366621fe630", "muratcantuna9@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA9@GMAIL.COM", "MURATCANTUNA9", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna9" },
                    { "673c4176-18c5-4ac8-b3c8-49e2775db350", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "2bdebbaf-0c84-4750-87a0-4be4fe3a63f7", "muratcantuna10@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, null, "MURATCANTUNA10@GMAIL.COM", "MURATCANTUNA10", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
