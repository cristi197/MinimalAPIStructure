using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndroleAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e01ff5d-a741-4f34-90b4-4c8a5b9c7dac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc9f0e5c-ec47-43e7-ab7b-8e61b3cf982c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15d2bddc-2d9d-4fab-9ad2-acea8533e0b8", null, "User", "USER" },
                    { "3f96c1ea-6bd6-4c2e-b7b5-7f64e9b7ac42", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, "fcc32a50-c796-4fe4-8e73-a0624eb6060e", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDezoEXzNQTVpy60H/rHtj/pnkqBaHJKY/7vEMmngFBixqgqtC0MxG7z2VflvAeTUA==", null, false, "0045db0e-f38c-49ed-9963-86c976809238", false, "schooluser@localhost.com" },
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, "f7a349ad-8307-46dd-9067-b169f5f7ff9f", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAELwbnLrGIlYqpgCGSD22f9v0YMyyplMX84uLfIMZnxy6qBEkr5SCLlI/uM3CbiAXiw==", null, false, "94eeec0f-5f0c-4f21-954e-228f428d84cf", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "15d2bddc-2d9d-4fab-9ad2-acea8533e0b8", "3f4631bd-f907-4409-b416-ba356312e659" },
                    { "3f96c1ea-6bd6-4c2e-b7b5-7f64e9b7ac42", "408aa945-3d84-4421-8342-7269ec64d949" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "15d2bddc-2d9d-4fab-9ad2-acea8533e0b8", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f96c1ea-6bd6-4c2e-b7b5-7f64e9b7ac42", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15d2bddc-2d9d-4fab-9ad2-acea8533e0b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f96c1ea-6bd6-4c2e-b7b5-7f64e9b7ac42");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e01ff5d-a741-4f34-90b4-4c8a5b9c7dac", null, "Administrator", "ADMINISTRATOR" },
                    { "dc9f0e5c-ec47-43e7-ab7b-8e61b3cf982c", null, "User", "USER" }
                });
        }
    }
}
