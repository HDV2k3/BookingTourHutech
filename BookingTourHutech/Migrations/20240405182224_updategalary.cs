using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTourHutech.Migrations
{
    /// <inheritdoc />
    public partial class updategalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GallaryDescription",
                table: "gallaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GallaryName",
                table: "gallaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonGallary",
                table: "gallaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GallaryDescription",
                table: "gallaries");

            migrationBuilder.DropColumn(
                name: "GallaryName",
                table: "gallaries");

            migrationBuilder.DropColumn(
                name: "PersonGallary",
                table: "gallaries");
        }
    }
}
