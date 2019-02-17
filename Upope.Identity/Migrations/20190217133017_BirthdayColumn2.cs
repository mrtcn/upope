using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Identity.Migrations
{
    public partial class BirthdayColumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "dfacb498-e2cf-4195-bf1d-035918675e8c", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "587b9c25-b1c3-4816-9dc2-f6d883280cf3", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA1@GMAIL.COM", "MURATCANTUNA1", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna1" },
                    { "99f8f578-b00f-483e-aed4-d34a92dd665d", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "004689a0-352a-45ea-abf4-85ffc9b8daf1", "muratcantuna2@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA2@GMAIL.COM", "MURATCANTUNA2", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna2" },
                    { "141c644a-7aee-431a-b2ef-96809189373c", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "0a19aa2a-c25a-427f-b544-72f4cf8c63ac", "muratcantuna3@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA3@GMAIL.COM", "MURATCANTUNA3", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna3" },
                    { "94731436-cd76-4845-808f-041db4965ebc", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "423adb42-5449-4c15-b798-7c8d64dac83f", "muratcantuna4@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA4@GMAIL.COM", "MURATCANTUNA4", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna4" },
                    { "d5fe2d42-f323-4742-ad6e-069d251223af", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "177933fb-eb9b-4310-a679-2c20bf3ee704", "muratcantuna5@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA5@GMAIL.COM", "MURATCANTUNA5", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna5" },
                    { "91eb6968-19ac-48e0-afbc-29e80139a955", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "029142f3-90c1-4f39-9c5c-e77efe5c6e3d", "muratcantuna6@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA6@GMAIL.COM", "MURATCANTUNA6", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna6" },
                    { "fa222a86-ef7c-4bde-ab3d-ad61901dadbd", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "1754ff59-8d68-4ec2-b577-633fafffb9a3", "muratcantuna7@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA7@GMAIL.COM", "MURATCANTUNA7", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna7" },
                    { "a2582b18-e7b8-4d45-b514-78f670d85aa9", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "4bf4094b-d5b7-43a2-b809-557868a2866f", "muratcantuna8@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA8@GMAIL.COM", "MURATCANTUNA8", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna8" },
                    { "4790b8d3-a478-4794-932f-0045ddcdba5c", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "662c5e34-9c02-4cf2-85d1-8efaf3ead5b4", "muratcantuna9@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA9@GMAIL.COM", "MURATCANTUNA9", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna9" },
                    { "692603a9-5181-4021-ae94-1c1fb80b0ae4", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "5c228b08-fad2-4483-a266-1581217d9cf7", "muratcantuna10@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA10@GMAIL.COM", "MURATCANTUNA10", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "141c644a-7aee-431a-b2ef-96809189373c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4790b8d3-a478-4794-932f-0045ddcdba5c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "692603a9-5181-4021-ae94-1c1fb80b0ae4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91eb6968-19ac-48e0-afbc-29e80139a955");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94731436-cd76-4845-808f-041db4965ebc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99f8f578-b00f-483e-aed4-d34a92dd665d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2582b18-e7b8-4d45-b514-78f670d85aa9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5fe2d42-f323-4742-ad6e-069d251223af");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dfacb498-e2cf-4195-bf1d-035918675e8c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa222a86-ef7c-4bde-ab3d-ad61901dadbd");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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
    }
}
