using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyWorkloadEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Faculty",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyWorkloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    DeliveryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursPerWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalStudents = table.Column<int>(type: "int", nullable: true),
                    CoordinationRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectHours = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyWorkloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyWorkloads_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FacultyWorkloads_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkloads_CourseId",
                table: "FacultyWorkloads",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkloads_FacultyEmail",
                table: "FacultyWorkloads",
                column: "FacultyEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Departments_DepartmentId",
                table: "Faculty",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Departments_DepartmentId",
                table: "Faculty");

            migrationBuilder.DropTable(
                name: "FacultyWorkloads");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Faculty");
        }
    }
}
