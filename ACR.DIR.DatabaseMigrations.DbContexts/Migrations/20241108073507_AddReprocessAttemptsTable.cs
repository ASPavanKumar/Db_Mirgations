using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACR.DIR.DatabaseMigrations.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class AddReprocessAttemptsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reprocessAttemptCount",
                table: "dcmObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "reprocessAttemptId",
                table: "dcmObjects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "reprocessAttempts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    configPath = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    objectCount = table.Column<long>(type: "bigint", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reprocessAttempts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "fk_dcmObjects_reprocessAttemptId_idx",
                table: "dcmObjects",
                column: "reprocessAttemptId");

            migrationBuilder.AddForeignKey(
                name: "fk_dcmObjects_reprocessAttempts_id",
                table: "dcmObjects",
                column: "reprocessAttemptId",
                principalTable: "reprocessAttempts",
                principalColumn: "id");

            migrationBuilder.CreateIndex(
                name: "dirTransaction_createdDateUtc_idx",
                table: "dirTransaction",
                column: "createdDateUtc");

            migrationBuilder.CreateIndex(
                name: "dcmObject_createdDateUtc_idx",
                table: "dcmObjects",
                column: "createdDateUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dcmObjects_reprocessAttempts_id",
                table: "dcmObjects");

            migrationBuilder.DropTable(
                name: "reprocessAttempts");

            migrationBuilder.DropIndex(
                name: "fk_dcmObjects_reprocessAttemptId_idx",
                table: "dcmObjects");

            migrationBuilder.DropColumn(
                name: "reprocessAttemptCount",
                table: "dcmObjects");

            migrationBuilder.DropColumn(
                name: "reprocessAttemptId",
                table: "dcmObjects");

            migrationBuilder.DropIndex(
               name: "dirTransaction_createdDateUtc_idx",
               table: "dirTransaction");

            migrationBuilder.DropIndex(
                name: "dcmObject_createdDateUtc_idx",
                table: "dcmObjects");

        }
    }
}
