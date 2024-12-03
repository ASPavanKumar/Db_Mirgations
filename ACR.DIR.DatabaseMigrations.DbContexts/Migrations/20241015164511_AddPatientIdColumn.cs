using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACR.DIR.DatabaseMigrations.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "patientId",
                table: "dcmStudies",
                type: "varchar(70)",
                maxLength: 70,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "patientId",
                table: "dcmStudies");
        }
    }
}
