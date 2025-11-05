using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramDetailsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationYears",
                table: "ProgramsOfStudy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "ProgramsOfStudy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Tuition",
                table: "ProgramsOfStudy",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "WorkloadHours",
                table: "ProgramsOfStudy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationYears",
                table: "ProgramsOfStudy");

            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "ProgramsOfStudy");

            migrationBuilder.DropColumn(
                name: "Tuition",
                table: "ProgramsOfStudy");

            migrationBuilder.DropColumn(
                name: "WorkloadHours",
                table: "ProgramsOfStudy");
        }
    }
}
