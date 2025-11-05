using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class Addedtheprogramanddeptrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ProgramsOfStudy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramsOfStudy_DepartmentId",
                table: "ProgramsOfStudy",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramsOfStudy_Departments_DepartmentId",
                table: "ProgramsOfStudy",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramsOfStudy_Departments_DepartmentId",
                table: "ProgramsOfStudy");

            migrationBuilder.DropIndex(
                name: "IX_ProgramsOfStudy_DepartmentId",
                table: "ProgramsOfStudy");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ProgramsOfStudy");
        }
    }
}
