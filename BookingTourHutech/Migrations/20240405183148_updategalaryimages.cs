using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class updategalaryimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalaryImages",
                columns: table => new
                {
                    IdGalaryImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGallary = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalaryImages", x => x.IdGalaryImage);
                    table.ForeignKey(
                        name: "FK_GalaryImages_gallaries_IdGallary",
                        column: x => x.IdGallary,
                        principalTable: "gallaries",
                        principalColumn: "IdGallary",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalaryImages_IdGallary",
                table: "GalaryImages",
                column: "IdGallary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalaryImages");
        }
    }
}
