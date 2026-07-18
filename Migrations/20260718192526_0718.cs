using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class _0718 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceMUNInstanceId_DelegationInstanceId",
                table: "DelegationCouncil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegationInstance",
                table: "DelegationInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegationCouncil",
                table: "DelegationCouncil");

            migrationBuilder.DropColumn(
                name: "DelegationInstanceMUNInstanceId",
                table: "DelegationCouncil");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegationInstance",
                table: "DelegationInstance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegationCouncil",
                table: "DelegationCouncil",
                columns: new[] { "DelegationInstanceId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_DelegationInstance_MUNInstanceId",
                table: "DelegationInstance",
                column: "MUNInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceId",
                table: "DelegationCouncil",
                column: "DelegationInstanceId",
                principalTable: "DelegationInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceId",
                table: "DelegationCouncil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegationInstance",
                table: "DelegationInstance");

            migrationBuilder.DropIndex(
                name: "IX_DelegationInstance_MUNInstanceId",
                table: "DelegationInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DelegationCouncil",
                table: "DelegationCouncil");

            migrationBuilder.AddColumn<int>(
                name: "DelegationInstanceMUNInstanceId",
                table: "DelegationCouncil",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegationInstance",
                table: "DelegationInstance",
                columns: new[] { "MUNInstanceId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DelegationCouncil",
                table: "DelegationCouncil",
                columns: new[] { "DelegationInstanceMUNInstanceId", "DelegationInstanceId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceMUNInstanceId_DelegationInstanceId",
                table: "DelegationCouncil",
                columns: new[] { "DelegationInstanceMUNInstanceId", "DelegationInstanceId" },
                principalTable: "DelegationInstance",
                principalColumns: new[] { "MUNInstanceId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
