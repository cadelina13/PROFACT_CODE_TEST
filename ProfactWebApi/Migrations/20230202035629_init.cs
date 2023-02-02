using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfactWebApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boundaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boundaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boundaries_Coordinates_CoordinateId",
                        column: x => x.CoordinateId,
                        principalTable: "Coordinates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NortheastId = table.Column<int>(type: "int", nullable: true),
                    SouthwestId = table.Column<int>(type: "int", nullable: true),
                    Zoom = table.Column<int>(type: "int", nullable: false),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostDatas_Coordinates_NortheastId",
                        column: x => x.NortheastId,
                        principalTable: "Coordinates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostDatas_Coordinates_SouthwestId",
                        column: x => x.SouthwestId,
                        principalTable: "Coordinates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boundaries_CoordinateId",
                table: "Boundaries",
                column: "CoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDatas_NortheastId",
                table: "PostDatas",
                column: "NortheastId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDatas_SouthwestId",
                table: "PostDatas",
                column: "SouthwestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boundaries");

            migrationBuilder.DropTable(
                name: "PostDatas");

            migrationBuilder.DropTable(
                name: "Coordinates");
        }
    }
}
