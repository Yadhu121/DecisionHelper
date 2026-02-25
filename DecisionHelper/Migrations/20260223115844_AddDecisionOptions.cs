using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddDecisionOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlairId = table.Column<int>(type: "int", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecisionOptions_Flairs_FlairId",
                        column: x => x.FlairId,
                        principalTable: "Flairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecisionOptions_FlairId",
                table: "DecisionOptions",
                column: "FlairId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecisionOptions");
        }
    }
}
