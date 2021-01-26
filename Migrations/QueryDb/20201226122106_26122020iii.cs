using Microsoft.EntityFrameworkCore.Migrations;

namespace BillerClientConsole.Migrations.QueryDb
{
    public partial class _26122020iii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueryCount",
                table: "QueryHistory");

            migrationBuilder.AddColumn<int>(
                name: "QueryCount",
                table: "Queries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueryCount",
                table: "Queries");

            migrationBuilder.AddColumn<int>(
                name: "QueryCount",
                table: "QueryHistory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
