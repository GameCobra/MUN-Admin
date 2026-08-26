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
                name: "CouncilInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MUNInstanceId = table.Column<int>(type: "int", nullable: false),
                    CouncilName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouncilInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouncilInformation_MUNInstance_MUNInstanceId",
                        column: x => x.MUNInstanceId,
                        principalTable: "MUNInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DelegationInstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelegationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelegationAccsesCode = table.Column<int>(type: "int", nullable: false),
                    MUNInstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegationInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DelegationInstance_MUNInstance_MUNInstanceId",
                        column: x => x.MUNInstanceId,
                        principalTable: "MUNInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resolution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouncilInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resolution_CouncilInformation_CouncilInformationId",
                        column: x => x.CouncilInformationId,
                        principalTable: "CouncilInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DelegationCouncil",
                columns: table => new
                {
                    DelegationInstanceId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouncilId = table.Column<int>(type: "int", nullable: false),
                    AmendmentPoints = table.Column<int>(type: "int", nullable: false),
                    RebuttalPoints = table.Column<int>(type: "int", nullable: false),
                    RequestedRebuttal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegationCouncil", x => new { x.DelegationInstanceId, x.Id });
                    table.ForeignKey(
                        name: "FK_DelegationCouncil_CouncilInformation_CouncilId",
                        column: x => x.CouncilId,
                        principalTable: "CouncilInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DelegationCouncil_DelegationInstance_DelegationInstanceId",
                        column: x => x.DelegationInstanceId,
                        principalTable: "DelegationInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ammendment",
                columns: table => new
                {
                    DelegationCouncilDelegationInstanceId = table.Column<int>(type: "int", nullable: false),
                    DelegationCouncilId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolutionID = table.Column<int>(type: "int", nullable: false),
                    ChangeClauseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammendment", x => new { x.DelegationCouncilDelegationInstanceId, x.DelegationCouncilId, x.Id });
                    table.ForeignKey(
                        name: "FK_Ammendment_DelegationCouncil_DelegationCouncilDelegationInstanceId_DelegationCouncilId",
                        columns: x => new { x.DelegationCouncilDelegationInstanceId, x.DelegationCouncilId },
                        principalTable: "DelegationCouncil",
                        principalColumns: new[] { "DelegationInstanceId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouncilInformation_MUNInstanceId",
                table: "CouncilInformation",
                column: "MUNInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DelegationCouncil_CouncilId",
                table: "DelegationCouncil",
                column: "CouncilId");

            migrationBuilder.CreateIndex(
                name: "IX_DelegationInstance_MUNInstanceId",
                table: "DelegationInstance",
                column: "MUNInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resolution_CouncilInformationId",
                table: "Resolution",
                column: "CouncilInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ammendment");

            migrationBuilder.DropTable(
                name: "Resolution");

            migrationBuilder.DropTable(
                name: "DelegationCouncil");

            migrationBuilder.DropTable(
                name: "CouncilInformation");

            migrationBuilder.DropTable(
                name: "DelegationInstance");

            migrationBuilder.DropTable(
                name: "MUNInstance");
        }
    }
}
