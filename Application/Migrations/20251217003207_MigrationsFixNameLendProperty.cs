using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsFixNameLendProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_AspNetUsers_CostumerUserId",
                table: "Lends");

            migrationBuilder.RenameColumn(
                name: "CostumerUserId",
                table: "Lends",
                newName: "CustumerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_CostumerUserId",
                table: "Lends",
                newName: "IX_Lends_CustumerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_AspNetUsers_CustumerUserId",
                table: "Lends",
                column: "CustumerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_AspNetUsers_CustumerUserId",
                table: "Lends");

            migrationBuilder.RenameColumn(
                name: "CustumerUserId",
                table: "Lends",
                newName: "CostumerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_CustumerUserId",
                table: "Lends",
                newName: "IX_Lends_CostumerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_AspNetUsers_CostumerUserId",
                table: "Lends",
                column: "CostumerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
