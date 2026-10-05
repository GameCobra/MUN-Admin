using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class resolutionAmmendmentInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptingAmmendments",
                table: "Resolution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxAmmendmentsPerDelegaton",
                table: "Resolution",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptingAmmendments",
                table: "Resolution");

            migrationBuilder.DropColumn(
                name: "MaxAmmendmentsPerDelegaton",
                table: "Resolution");
        }
    }
}
