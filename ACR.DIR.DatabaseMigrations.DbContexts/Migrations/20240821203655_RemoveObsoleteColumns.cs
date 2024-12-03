using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACR.DIR.DatabaseMigrations.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class RemoveObsoleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdUser",
                table: "dcmStudies");

            migrationBuilder.DropColumn(
                name: "lastModifiedUser",
                table: "dcmStudies");

            migrationBuilder.DropColumn(
                name: "createdUser",
                table: "dcmSeries");

            migrationBuilder.DropColumn(
                name: "lastModifiedUser",
                table: "dcmSeries");

            migrationBuilder.DropColumn(
                name: "fileName",
                table: "dcmObjects");

            migrationBuilder.DropColumn(
                name: "lastModifiedUser",
                table: "dcmObjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdUser",
                table: "dcmStudies",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "lastModifiedUser",
                table: "dcmStudies",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdUser",
                table: "dcmSeries",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "lastModifiedUser",
                table: "dcmSeries",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "fileName",
                table: "dcmObjects",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "lastModifiedUser",
                table: "dcmObjects",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
