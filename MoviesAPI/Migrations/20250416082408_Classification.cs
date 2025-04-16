using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class Classification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalRouteImage",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Clasification",
                table: "Movies",
                newName: "Classification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Classification",
                table: "Movies",
                newName: "Clasification");

            migrationBuilder.AddColumn<string>(
                name: "LocalRouteImage",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
