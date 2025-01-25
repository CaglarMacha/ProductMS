using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AuditLoggings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AuditLoggings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AuditLoggings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AuditLoggings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AuditLoggings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AuditLoggings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AuditLoggings",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AuditLoggings");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AuditLoggings");
        }
    }
}
