using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCouncilInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptingAmmendments",
                table: "CouncilInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptingAmmendments",
                table: "CouncilInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
