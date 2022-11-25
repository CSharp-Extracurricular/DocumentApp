using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentApp.Infrastructure.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitationIndex_Publications_PublicationId",
                table: "CitationIndex");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorGroupId",
                table: "Publications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DOI",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "CitationIndex",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CitationIndex_Publications_PublicationId",
                table: "CitationIndex",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitationIndex_Publications_PublicationId",
                table: "CitationIndex");

            migrationBuilder.DropColumn(
                name: "AuthorGroupId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "DOI",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Author");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "CitationIndex",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CitationIndex_Publications_PublicationId",
                table: "CitationIndex",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id");
        }
    }
}
