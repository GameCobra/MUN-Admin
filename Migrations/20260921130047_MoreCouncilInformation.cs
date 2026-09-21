using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class MoreCouncilInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentActivity",
                table: "CouncilInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentSpeakingCountry",
                table: "CouncilInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RebuttalingCountry",
                table: "CouncilInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentActivity",
                table: "CouncilInformation");

            migrationBuilder.DropColumn(
                name: "CurrentSpeakingCountry",
                table: "CouncilInformation");

            migrationBuilder.DropColumn(
                name: "RebuttalingCountry",
                table: "CouncilInformation");
        }
    }
}
