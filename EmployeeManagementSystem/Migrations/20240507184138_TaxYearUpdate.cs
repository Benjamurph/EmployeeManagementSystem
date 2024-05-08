using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class TaxYearUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TaxYears");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TaxYears",
                newName: "Year");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalRateTaxPercentage",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalRateThreshold",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasicRateLowerThreshold",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasicRateTaxPercentage",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasicRateUpperThreshold",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HigherRateLowerThreshold",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HigherRateTaxPercentage",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HigherRateUpperThreshold",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonalAllowance",
                table: "TaxYears",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalRateTaxPercentage",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "AdditionalRateThreshold",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "BasicRateLowerThreshold",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "BasicRateTaxPercentage",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "BasicRateUpperThreshold",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "HigherRateLowerThreshold",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "HigherRateTaxPercentage",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "HigherRateUpperThreshold",
                table: "TaxYears");

            migrationBuilder.DropColumn(
                name: "PersonalAllowance",
                table: "TaxYears");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "TaxYears",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TaxYears",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
