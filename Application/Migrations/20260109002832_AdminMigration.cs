using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class AdminMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f155c2c-a272-4b45-9ebf-3a68d13e8e21");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Document", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "29E23309-9F18-4652-8279-E86EA6B634CC", 0, "8ce0715e-445f-4c8e-bd09-0bcce3f8d1a5", "1234567", "admin@library.com", true, false, null, "ADMIN", "ADMIN@LIBRARY.COM", "ADMIN", "AQAAAAIAAYagAAAAEPmoLAivp1UhCfEvee91VdwKu3hAVOj9E6esiW/aPy0O/w/06FlLHkQeyerxfK8POw==", null, false, "82fd216c-3bdd-407d-8187-ef7dde88476a", false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1A7E2F64-9A1E-4A9E-9B57-2F3028E3A02D", "29E23309-9F18-4652-8279-E86EA6B634CC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1A7E2F64-9A1E-4A9E-9B57-2F3028E3A02D", "29E23309-9F18-4652-8279-E86EA6B634CC" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29E23309-9F18-4652-8279-E86EA6B634CC");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Document", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2f155c2c-a272-4b45-9ebf-3a68d13e8e21", 0, "b00b31c7-4697-4863-9b04-833e3ede2d04", "1234567", "admin@library.com", false, false, null, "ADMIN", "ADMIN@LIBRARY.COM", "ADMIN", "AQAAAAIAAYagAAAAEIuvH+W5egAZAaUNozKs+M1QmxbuGdPQkWYBjR+BPbiuAIGl0ju2qzc5185YBBSgkA==", null, false, "99cdeefe-5bd2-4c1e-940a-73e54c31bcbe", false, "ADMIN" });
        }
    }
}
