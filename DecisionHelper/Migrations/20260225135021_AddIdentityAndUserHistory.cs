using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityAndUserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriterionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHigher = table.Column<bool>(type: "bit", nullable: false),
                    FlairId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCriteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDecisionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlairId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDecisionOptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCriteria");

            migrationBuilder.DropTable(
                name: "UserDecisionOptions");
        }
    }
}
