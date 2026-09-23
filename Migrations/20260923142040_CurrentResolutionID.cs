using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class CurrentResolutionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentResolutionID",
                table: "CouncilInformation",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentResolutionID",
                table: "CouncilInformation");
        }
    }
}
