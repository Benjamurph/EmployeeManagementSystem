using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class JobsBoardParagraphs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Paragraph1",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paragraph2",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paragraph3",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paragraph4",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paragraph5",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading1",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading2",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading3",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading4",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading5",
                table: "JobBoards",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paragraph1",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "Paragraph2",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "Paragraph3",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "Paragraph4",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "Paragraph5",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "SubHeading1",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "SubHeading2",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "SubHeading3",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "SubHeading4",
                table: "JobBoards");

            migrationBuilder.DropColumn(
                name: "SubHeading5",
                table: "JobBoards");
        }
    }
}
