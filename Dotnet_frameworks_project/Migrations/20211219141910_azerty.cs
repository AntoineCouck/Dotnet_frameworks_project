using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet_frameworks_project.Migrations
{
    public partial class azerty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsuranceId",
                table: "Patient",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Min_age = table.Column<int>(type: "int", nullable: false),
                    Max_age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PassedTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassedTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassedTests_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PassedTests_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_InsuranceId",
                table: "Patient",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PassedTests_PatientId",
                table: "PassedTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PassedTests_TestId",
                table: "PassedTests",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Insurance_InsuranceId",
                table: "Patient",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Insurance_InsuranceId",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "PassedTests");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Patient_InsuranceId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Patient");
        }
    }
}
