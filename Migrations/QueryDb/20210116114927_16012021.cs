using Microsoft.EntityFrameworkCore.Migrations;

namespace BillerClientConsole.Migrations.QueryDb
{
    public partial class _16012021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "applicationID",
                table: "Queries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applicationID",
                table: "Queries");
        }
    }
}
