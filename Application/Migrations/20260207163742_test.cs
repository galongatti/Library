using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29E23309-9F18-4652-8279-E86EA6B634CC",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e443304-a28a-43fc-a7c9-5c9d92935fe0", "AQAAAAIAAYagAAAAEBp32Jsre9GYHsj2W/qi5B1tDKdkzTEtFJFkFD/UUgaG4MnoohMWYThj/khJReLYXA==", "31826443-2b93-4ee7-b6a9-d24acb6067e5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29E23309-9F18-4652-8279-E86EA6B634CC",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ce0715e-445f-4c8e-bd09-0bcce3f8d1a5", "AQAAAAIAAYagAAAAEPmoLAivp1UhCfEvee91VdwKu3hAVOj9E6esiW/aPy0O/w/06FlLHkQeyerxfK8POw==", "82fd216c-3bdd-407d-8187-ef7dde88476a" });
        }
    }
}
