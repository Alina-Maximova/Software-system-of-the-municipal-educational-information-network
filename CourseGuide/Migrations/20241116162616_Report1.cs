using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseGuide.Migrations
{
    /// <inheritdoc />
    public partial class Report1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducationalInstitutionId = table.Column<int>(type: "integer", nullable: true),
                    AcademicYear = table.Column<string>(type: "text", nullable: false),
                    StudentsAdmitted = table.Column<int>(type: "integer", nullable: false),
                    StudentsGraduated = table.Column<int>(type: "integer", nullable: false),
                    NumberOfTeachers = table.Column<int>(type: "integer", nullable: false),
                    Revenue = table.Column<decimal>(type: "numeric", nullable: false),
                    FreeClasses = table.Column<int>(type: "integer", nullable: false),
                    AgeGroup = table.Column<string>(type: "text", nullable: false),
                    PlansNextYear = table.Column<string>(type: "text", nullable: false),
                    Challenges = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_EducationalInstitutions_EducationalInstitutionId",
                        column: x => x.EducationalInstitutionId,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EducationalInstitutionId",
                table: "Reports",
                column: "EducationalInstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
