using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class Yay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkloadType",
                table: "Workloads");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hours",
                table: "Workloads",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workloads",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Workloads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProgramOfStudyId",
                table: "Workloads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkloadCategoryId",
                table: "Workloads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_ProgramOfStudyId",
                table: "Workloads",
                column: "ProgramOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_WorkloadCategoryId",
                table: "Workloads",
                column: "WorkloadCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workloads_ProgramsOfStudy_ProgramOfStudyId",
                table: "Workloads",
                column: "ProgramOfStudyId",
                principalTable: "ProgramsOfStudy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workloads_WorkloadCategories_WorkloadCategoryId",
                table: "Workloads",
                column: "WorkloadCategoryId",
                principalTable: "WorkloadCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workloads_ProgramsOfStudy_ProgramOfStudyId",
                table: "Workloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Workloads_WorkloadCategories_WorkloadCategoryId",
                table: "Workloads");

            migrationBuilder.DropIndex(
                name: "IX_Workloads_ProgramOfStudyId",
                table: "Workloads");

            migrationBuilder.DropIndex(
                name: "IX_Workloads_WorkloadCategoryId",
                table: "Workloads");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Workloads");

            migrationBuilder.DropColumn(
                name: "ProgramOfStudyId",
                table: "Workloads");

            migrationBuilder.DropColumn(
                name: "WorkloadCategoryId",
                table: "Workloads");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hours",
                table: "Workloads",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workloads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkloadType",
                table: "Workloads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
