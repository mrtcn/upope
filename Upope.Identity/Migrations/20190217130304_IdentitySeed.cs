using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Identity.Migrations
{
    public partial class IdentitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efe709a7-7269-429f-ae0f-2997432e9b77");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "38fdfc79-1707-42e0-8ff5-6d28a5505824", 0, "220692e9-18a9-40ca-b82d-8ebe48ac6fb3", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA1@GMAIL.COM", "MURATCANTUNA1", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna1" },
                    { "8debfdd1-d35a-4a11-bdd0-931fc5b7938e", 0, "8c05e272-8835-40f9-a2de-df7adfa60bd0", "muratcantuna2@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA2@GMAIL.COM", "MURATCANTUNA2", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna2" },
                    { "ecd204e8-4e62-4a7f-8b00-b3e8edbfa87d", 0, "0e7884ba-0275-4517-999e-50e089051ccf", "muratcantuna3@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA3@GMAIL.COM", "MURATCANTUNA3", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna3" },
                    { "23e722fc-58f0-4e0e-b2a8-e17995fe0f75", 0, "5ae996f2-3c16-492b-8b44-96b22ec83ccb", "muratcantuna4@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA4@GMAIL.COM", "MURATCANTUNA4", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna4" },
                    { "8d4e94f1-7af1-4410-9a98-8e8f1d4837cb", 0, "6dc9444e-a527-489e-bdb2-0dfbbd379187", "muratcantuna5@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA5@GMAIL.COM", "MURATCANTUNA5", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna5" },
                    { "954aa953-0238-447e-84ad-3f93002e57ba", 0, "689800b6-414b-4668-924d-3ea9bfac16d6", "muratcantuna6@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA6@GMAIL.COM", "MURATCANTUNA6", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna6" },
                    { "4361fa5d-1c6f-4cc4-8179-8e146b5d3169", 0, "0cd88d89-bd90-45be-bc73-1b951aeb3ea4", "muratcantuna7@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA7@GMAIL.COM", "MURATCANTUNA7", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna7" },
                    { "0452a935-1339-41bf-9810-67af374463ee", 0, "acc6aa5f-d17b-4c40-b8af-0d499c7a7959", "muratcantuna8@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA8@GMAIL.COM", "MURATCANTUNA8", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna8" },
                    { "75234d08-19f1-4811-bfba-18fab85da5b2", 0, "f3a9c783-ae01-4961-9ab6-03745a071111", "muratcantuna9@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA9@GMAIL.COM", "MURATCANTUNA9", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna9" },
                    { "85360474-9f83-4b91-b870-296cbe50c5d8", 0, "75311b71-561f-49be-872f-f80e1a0f2da4", "muratcantuna10@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA10@GMAIL.COM", "MURATCANTUNA10", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0452a935-1339-41bf-9810-67af374463ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23e722fc-58f0-4e0e-b2a8-e17995fe0f75");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38fdfc79-1707-42e0-8ff5-6d28a5505824");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4361fa5d-1c6f-4cc4-8179-8e146b5d3169");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75234d08-19f1-4811-bfba-18fab85da5b2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85360474-9f83-4b91-b870-296cbe50c5d8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d4e94f1-7af1-4410-9a98-8e8f1d4837cb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8debfdd1-d35a-4a11-bdd0-931fc5b7938e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "954aa953-0238-447e-84ad-3f93002e57ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ecd204e8-4e62-4a7f-8b00-b3e8edbfa87d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "efe709a7-7269-429f-ae0f-2997432e9b77", 0, "5c60052c-fa50-4a93-85c0-64110a907fc2", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", false, null, "MURATCANTUNA1@GMAIL.COM", null, null, null, false, null, null, false, "muratcantuna1" });
        }
    }
}
