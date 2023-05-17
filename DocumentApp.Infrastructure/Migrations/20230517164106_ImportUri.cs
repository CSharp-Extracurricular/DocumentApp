using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImportUri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImportUri",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportUri",
                table: "Publications");
        }
    }
}
