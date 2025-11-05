using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkloadCategoryToFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkloadCategoryId",
                table: "Faculty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_WorkloadCategoryId",
                table: "Faculty",
                column: "WorkloadCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_WorkloadCategories_WorkloadCategoryId",
                table: "Faculty",
                column: "WorkloadCategoryId",
                principalTable: "WorkloadCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_WorkloadCategories_WorkloadCategoryId",
                table: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_WorkloadCategoryId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "WorkloadCategoryId",
                table: "Faculty");
        }
    }
}
