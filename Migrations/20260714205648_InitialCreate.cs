using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MUNAdmin.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MUNInstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MUNTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MUNAccessCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DelegationInstance",
                columns: table => new
                {
                    MUNInstanceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelegationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelegationAccsesCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegationInstance", x => new { x.MUNInstanceId, x.Id });
                    table.ForeignKey(
                        name: "FK_DelegationInstance_MUNInstance_MUNInstanceId",
                        column: x => x.MUNInstanceId,
                        principalTable: "MUNInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MUNInstance_CouncilInformationList",
                columns: table => new
                {
                    MUNInstanceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouncilName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNInstance_CouncilInformationList", x => new { x.MUNInstanceId, x.Id });
                    table.ForeignKey(
                        name: "FK_MUNInstance_CouncilInformationList_MUNInstance_MUNInstanceId",
                        column: x => x.MUNInstanceId,
                        principalTable: "MUNInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DelegationCouncil",
                columns: table => new
                {
                    DelegationInstanceMUNInstanceId = table.Column<int>(type: "int", nullable: false),
                    DelegationInstanceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Council_CouncilName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmendmentPoints = table.Column<int>(type: "int", nullable: false),
                    RebuttalPoints = table.Column<int>(type: "int", nullable: false),
                    RequestedRebuttal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegationCouncil", x => new { x.DelegationInstanceMUNInstanceId, x.DelegationInstanceId, x.Id });
                    table.ForeignKey(
                        name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceMUNInstanceId_DelegationInstanceId",
                        columns: x => new { x.DelegationInstanceMUNInstanceId, x.DelegationInstanceId },
                        principalTable: "DelegationInstance",
                        principalColumns: new[] { "MUNInstanceId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelegationCouncil");

            migrationBuilder.DropTable(
                name: "MUNInstance_CouncilInformationList");

            migrationBuilder.DropTable(
                name: "DelegationInstance");

            migrationBuilder.DropTable(
                name: "MUNInstance");
        }
    }
}
