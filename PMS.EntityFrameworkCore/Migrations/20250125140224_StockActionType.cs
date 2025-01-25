using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class StockActionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockActionTypes",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockActionTypes",
                table: "Stocks");
        }
    }
}
