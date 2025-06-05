using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "Budgets",
                        principalTable: "Budget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BudgetId",
                schema: "Budgets",
                table: "Category",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                schema: "Budgets",
                table: "Category",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Type",
                schema: "Budgets",
                table: "Category",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category",
                schema: "Budgets");
        }
    }
}
