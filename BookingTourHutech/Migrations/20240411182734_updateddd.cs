using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class updateddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "BookingTours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "BookingTours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
