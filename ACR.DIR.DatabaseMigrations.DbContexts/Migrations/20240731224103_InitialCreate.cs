using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACR.DIR.DatabaseMigrations.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dcmStudies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studyInstanceUid = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    time = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    accessionNumber = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    studyId = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDateUTC = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastModifiedUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dcmStudies", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dirTransaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cbsTransactionId = table.Column<long>(type: "bigint", nullable: true),
                    facilityId = table.Column<long>(type: "bigint", nullable: false),
                    corporateId = table.Column<long>(type: "bigint", nullable: false),
                    userId = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorizationStatus = table.Column<long>(type: "bigint", nullable: false),
                    authorizationStatusLogs = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dirTransaction", x => x.id);
                    table.ForeignKey(
                            name: "fk_dirTransaction_transaction_id",
                            column: x => x.cbsTransactionId,
                            principalTable: "transactions",
                            principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dcmSeries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    seriesInstanceUid = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dcmStudyId = table.Column<long>(type: "bigint", nullable: true),
                    modality = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seriesNumber = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastModifiedUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dcmSeries", x => x.id);
                    table.ForeignKey(
                        name: "fk_dcmSeries_dcmStudies_id",
                        column: x => x.dcmStudyId,
                        principalTable: "dcmStudies",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dcmObjects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sopInstanceUid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sopClassUid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    instanceNumber = table.Column<int>(type: "int", nullable: true),
                    dcmSeriesId = table.Column<long>(type: "bigint", nullable: true),
                    attributes = table.Column<string>(type: "JSON", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    fileName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastModifiedUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    s3Path = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dirTransactionId = table.Column<long>(type: "bigint", nullable: true),
                    cbsObjectId = table.Column<long>(type: "bigint", nullable: true),
                    objectStatus = table.Column<long>(type: "bigint", nullable: false),
                    objectStatusLogs = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dcmObjects", x => x.id);
                    table.ForeignKey(
                        name: "fk_dcmObjects_dcmSeries_id",
                        column: x => x.dcmSeriesId,
                        principalTable: "dcmSeries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_dcmObjects_dirTransaction_id",
                        column: x => x.dirTransactionId,
                        principalTable: "dirTransaction",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_dcmObjects_object_id",
                        column: x => x.cbsObjectId,
                        principalTable: "objects",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "dcmObject_cbsObjectId_idx",
                table: "dcmObjects",
                column: "cbsObjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_dcmObjects_dcmSeriesId_idx",
                table: "dcmObjects",
                column: "dcmSeriesId");

            migrationBuilder.CreateIndex(
                name: "fk_dcmObjects_dirTransactionId_idx",
                table: "dcmObjects",
                column: "dirTransactionId");

            migrationBuilder.CreateIndex(
                name: "dcmSeries_seriesInstanceUid_idx",
                table: "dcmSeries",
                column: "seriesInstanceUid");

            migrationBuilder.CreateIndex(
                name: "fk_dcmSeries_dcmStudyId_idx",
                table: "dcmSeries",
                column: "dcmStudyId");

            migrationBuilder.CreateIndex(
                name: "dcmStudies_studyInstanceUid_idx",
                table: "dcmStudies",
                column: "studyInstanceUid");

            migrationBuilder.CreateIndex(
                name: "dirTransaction_cbsTransactionid_idx",
                table: "dirTransaction",
                column: "cbsTransactionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dcmObjects");

            migrationBuilder.DropTable(
                name: "dcmSeries");

            migrationBuilder.DropTable(
                name: "dirTransaction");

            migrationBuilder.DropTable(
                name: "dcmStudies");
        }
    }
}
