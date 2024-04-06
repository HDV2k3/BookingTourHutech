using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class BookingTour1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeopleCount",
                table: "BookingTours",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "ActivationMethod",
                table: "BookingTours",
                newName: "transport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "transport",
                table: "BookingTours",
                newName: "ActivationMethod");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "BookingTours",
                newName: "PeopleCount");
        }
    }
}
