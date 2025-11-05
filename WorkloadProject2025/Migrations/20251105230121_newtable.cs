using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkloadType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workloads_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workloads_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workloads_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_CourseId",
                table: "Workloads",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_FacultyEmail",
                table: "Workloads",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_TermId",
                table: "Workloads",
                column: "TermId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workloads");
        }
    }
}
