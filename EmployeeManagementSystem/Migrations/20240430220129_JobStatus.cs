using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class JobStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_StatusId",
                table: "JobApplications",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_SystemCodeDetails_StatusId",
                table: "JobApplications",
                column: "StatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_SystemCodeDetails_StatusId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_StatusId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "JobApplications");
        }
    }
}
