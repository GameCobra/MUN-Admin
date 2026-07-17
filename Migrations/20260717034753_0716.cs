using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class _0716 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryColor",
                table: "MUNInstance_CouncilInformationList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryColor",
                table: "MUNInstance_CouncilInformationList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Council_PrimaryColor",
                table: "DelegationCouncil",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Council_SecondaryColor",
                table: "DelegationCouncil",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryColor",
                table: "MUNInstance_CouncilInformationList");

            migrationBuilder.DropColumn(
                name: "SecondaryColor",
                table: "MUNInstance_CouncilInformationList");

            migrationBuilder.DropColumn(
                name: "Council_PrimaryColor",
                table: "DelegationCouncil");

            migrationBuilder.DropColumn(
                name: "Council_SecondaryColor",
                table: "DelegationCouncil");
        }
    }
}
