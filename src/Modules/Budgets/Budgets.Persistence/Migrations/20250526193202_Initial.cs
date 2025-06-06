﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Budgets");

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Details_Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                schema: "Budgets",
                table: "Budget",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget",
                schema: "Budgets");
        }
    }
}
