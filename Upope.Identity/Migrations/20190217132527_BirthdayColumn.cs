using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Identity.Migrations
{
    public partial class BirthdayColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "93a24297-517c-4ad0-aabc-7a966596ef5e", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "032d3f7b-581a-4f69-94d0-88b23a09ff5f", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA1@GMAIL.COM", "MURATCANTUNA1", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna1" },
                    { "4d203499-b970-40a7-971d-615c5591954a", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d46e8582-d439-49c3-88ed-689674ebfa6a", "muratcantuna2@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA2@GMAIL.COM", "MURATCANTUNA2", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna2" },
                    { "9e2cce73-437c-48df-861a-32f30613d037", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b4a8150a-2ad8-44ec-9eec-d81cc1ad8741", "muratcantuna3@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA3@GMAIL.COM", "MURATCANTUNA3", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna3" },
                    { "4f138d8f-5455-4ea0-b0a3-abaf68d932a6", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2af35340-0a9f-47d5-a97c-318fcc81a6fb", "muratcantuna4@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA4@GMAIL.COM", "MURATCANTUNA4", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna4" },
                    { "618ccbe1-552d-4db6-9e96-f7f31ade1b3b", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4a345144-c543-4973-92c7-49793b784c9e", "muratcantuna5@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA5@GMAIL.COM", "MURATCANTUNA5", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna5" },
                    { "b356afb5-8bd4-4bc5-9fe7-69383b2dc510", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "96837039-80c1-4ea6-a61b-2e58d3649c54", "muratcantuna6@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA6@GMAIL.COM", "MURATCANTUNA6", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna6" },
                    { "6781d50a-2b32-4c86-9c31-fb36da3c3673", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "caa02093-c61c-4228-982c-ddc15de41f70", "muratcantuna7@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA7@GMAIL.COM", "MURATCANTUNA7", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna7" },
                    { "5a9e8b5c-d6a1-454c-92df-9f207b49c6fa", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "51352b74-ae9d-4dc6-a0fe-bcaa7e128c59", "muratcantuna8@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA8@GMAIL.COM", "MURATCANTUNA8", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna8" },
                    { "3b7347a3-4b25-4ae6-b8a6-8cb7f99b2a2c", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7b642fae-ea93-4ade-8d8f-e78c2ed7e0ff", "muratcantuna9@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA9@GMAIL.COM", "MURATCANTUNA9", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna9" },
                    { "25a8971b-b04d-4ddc-9fd3-01e194628bf2", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2e4aa312-48bd-43a0-9d56-93a52f171bf7", "muratcantuna10@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA10@GMAIL.COM", "MURATCANTUNA10", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25a8971b-b04d-4ddc-9fd3-01e194628bf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b7347a3-4b25-4ae6-b8a6-8cb7f99b2a2c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d203499-b970-40a7-971d-615c5591954a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f138d8f-5455-4ea0-b0a3-abaf68d932a6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5a9e8b5c-d6a1-454c-92df-9f207b49c6fa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "618ccbe1-552d-4db6-9e96-f7f31ade1b3b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6781d50a-2b32-4c86-9c31-fb36da3c3673");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93a24297-517c-4ad0-aabc-7a966596ef5e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e2cce73-437c-48df-861a-32f30613d037");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b356afb5-8bd4-4bc5-9fe7-69383b2dc510");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

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
    }
}
