using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "Budgets",
                table: "Budget",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                schema: "Budgets",
                table: "Budget",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_OwnerId",
                schema: "Budgets",
                table: "Budget");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Budgets",
                table: "Budget");
        }
    }
}
