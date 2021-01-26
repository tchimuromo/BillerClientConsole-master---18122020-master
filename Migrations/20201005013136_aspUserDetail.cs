using Microsoft.EntityFrameworkCore.Migrations;

namespace BillerClientConsole.Migrations
{
    public partial class aspUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsersDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    natid = table.Column<string>(nullable: true),
                    firstname = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsersDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsersInternal",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    natid = table.Column<string>(nullable: true),
                    pseudo = table.Column<string>(nullable: true),
                    firstname = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsersInternal", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsersDetails");

            migrationBuilder.DropTable(
                name: "AspNetUsersInternal");
        }
    }
}
