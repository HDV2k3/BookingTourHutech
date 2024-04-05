using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class updatetableimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailTourDescription",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "BookingTours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourImages",
                columns: table => new
                {
                    IdTourImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourImages", x => x.IdTourImage);
                    table.ForeignKey(
                        name: "FK_TourImages_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTours_TourId",
                table: "BookingTours",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourImages_TourId",
                table: "TourImages",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTours_Tours_TourId",
                table: "BookingTours",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTours_Tours_TourId",
                table: "BookingTours");

            migrationBuilder.DropTable(
                name: "TourImages");

            migrationBuilder.DropIndex(
                name: "IX_BookingTours_TourId",
                table: "BookingTours");

            migrationBuilder.DropColumn(
                name: "DetailTourDescription",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "BookingTours");
        }
    }
}
