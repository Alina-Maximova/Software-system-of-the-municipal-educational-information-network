using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseGuide.Migrations
{
    /// <inheritdoc />
    public partial class fileAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "EducationalInstitutions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "EducationalInstitutions");
        }
    }
}
