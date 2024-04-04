using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class initDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingTours",
                columns: table => new
                {
                    BookingTourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivationMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeopleCount = table.Column<int>(type: "int", nullable: false),
                    DayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTours", x => x.BookingTourId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTours",
                columns: table => new
                {
                    CategoryTourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryNameAlias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTours", x => x.CategoryTourId);
                });

            migrationBuilder.CreateTable(
                name: "Supliers",
                columns: table => new
                {
                    SuplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    deputyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneSuplier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressSuplier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionSuplier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supliers", x => x.SuplierId);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourPrice = table.Column<double>(type: "float", nullable: false),
                    CategoryTourId = table.Column<int>(type: "int", nullable: false),
                    SuplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tours_CategoryTours_CategoryTourId",
                        column: x => x.CategoryTourId,
                        principalTable: "CategoryTours",
                        principalColumn: "CategoryTourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Supliers_SuplierId",
                        column: x => x.SuplierId,
                        principalTable: "Supliers",
                        principalColumn: "SuplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailBookings",
                columns: table => new
                {
                    DetailBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingTourId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TourPrice = table.Column<double>(type: "float", nullable: false),
                    Disconut = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailBookings", x => x.DetailBookingId);
                    table.ForeignKey(
                        name: "FK_DetailBookings_BookingTours_BookingTourId",
                        column: x => x.BookingTourId,
                        principalTable: "BookingTours",
                        principalColumn: "BookingTourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailBookings_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailBookings_BookingTourId",
                table: "DetailBookings",
                column: "BookingTourId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBookings_TourId",
                table: "DetailBookings",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CategoryTourId",
                table: "Tours",
                column: "CategoryTourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_SuplierId",
                table: "Tours",
                column: "SuplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailBookings");

            migrationBuilder.DropTable(
                name: "BookingTours");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "CategoryTours");

            migrationBuilder.DropTable(
                name: "Supliers");
        }
    }
}
