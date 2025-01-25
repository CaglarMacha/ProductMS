using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class uniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categorys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Title_IsDeleted_IsActive",
                table: "Products",
                columns: new[] { "Title", "IsDeleted", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_Name_IsDeleted",
                table: "Categorys",
                columns: new[] { "Name", "IsDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Title_IsDeleted_IsActive",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categorys_Name_IsDeleted",
                table: "Categorys");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categorys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
