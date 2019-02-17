using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upope.Identity.Migrations
{
    public partial class ChallengeRequestRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "FirstName", "Gender", "GoogleId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "328c1309-cf33-45b8-97bf-88d4b7bbc2bd", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "80701080-e639-4bf7-b855-ecff85c67476", "muratcantuna1@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA1@GMAIL.COM", "MURATCANTUNA1", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna1" },
                    { "da62bff3-02be-4cd8-bb32-352e98a063a7", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "c17c4a36-3544-4ed5-98d8-7665b90c3ea6", "muratcantuna2@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA2@GMAIL.COM", "MURATCANTUNA2", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna2" },
                    { "c74cdb77-f3a8-4cdd-8cb7-3e98d33fa462", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "43fee1a5-66ce-4a4d-979d-4ce1454aa7f3", "muratcantuna3@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA3@GMAIL.COM", "MURATCANTUNA3", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna3" },
                    { "2074d2ab-72b7-4521-8e92-d6e0b822e512", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "a7588ad0-1bb9-4062-88a8-82b9cb7d7591", "muratcantuna4@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA4@GMAIL.COM", "MURATCANTUNA4", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna4" },
                    { "98791941-a1c5-4dd0-a45e-7da9fa1eb7cd", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "082d4471-3ed6-4be6-98a6-1fa109341ca1", "muratcantuna5@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA5@GMAIL.COM", "MURATCANTUNA5", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna5" },
                    { "08072102-865c-4323-9adf-21650226210b", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "edb1eb52-2640-4950-bfd7-16f3044dd044", "muratcantuna6@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA6@GMAIL.COM", "MURATCANTUNA6", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna6" },
                    { "7e065a0c-8fc6-4e1d-8994-ad4895400104", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "98250ddd-994b-4818-b28f-7b610795303e", "muratcantuna7@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA7@GMAIL.COM", "MURATCANTUNA7", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna7" },
                    { "10dde44f-b55a-4039-9eed-8445d10f302c", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "beae29a7-15ec-409d-8730-ab9bc20f0f22", "muratcantuna8@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA8@GMAIL.COM", "MURATCANTUNA8", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna8" },
                    { "60a349ed-d69a-464c-9296-83378ae449fa", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "8866703e-41a6-4529-bf8c-230a618da4e9", "muratcantuna9@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA9@GMAIL.COM", "MURATCANTUNA9", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna9" },
                    { "c2d5e928-b1a6-40de-adfd-a5656ead3da1", 0, new DateTime(1984, 12, 16, 12, 23, 26, 0, DateTimeKind.Unspecified), "164750c9-8f77-4cc4-8a1e-b97f02f1e5da", "muratcantuna10@gmail.com", true, null, "Murat", 1, null, "Tuna", true, null, "MURATCANTUNA10@GMAIL.COM", "MURATCANTUNA10", "AQAAAAEAACcQAAAAEGwm9j42G7b3gTRJJiYW4YoO2Rw8AezwRHbOOhW7jGjhP2JOQoAm++6csGS7kOVSlg==", null, false, null, "36IOAJVIVOFKWA6ZZRIC6RJCANRROVHD", false, "muratcantuna10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08072102-865c-4323-9adf-21650226210b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10dde44f-b55a-4039-9eed-8445d10f302c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2074d2ab-72b7-4521-8e92-d6e0b822e512");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "328c1309-cf33-45b8-97bf-88d4b7bbc2bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60a349ed-d69a-464c-9296-83378ae449fa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e065a0c-8fc6-4e1d-8994-ad4895400104");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98791941-a1c5-4dd0-a45e-7da9fa1eb7cd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c2d5e928-b1a6-40de-adfd-a5656ead3da1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c74cdb77-f3a8-4cdd-8cb7-3e98d33fa462");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da62bff3-02be-4cd8-bb32-352e98a063a7");

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
    }
}
