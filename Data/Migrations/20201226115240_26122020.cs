using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillerClientConsole.Data.Migrations
{
    public partial class _26122020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    QueryID = table.Column<Guid>(nullable: false),
                    applicationRef = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    dateCreated = table.Column<string>(nullable: true),
                    tableName = table.Column<string>(nullable: true),
                    emailAddress = table.Column<string>(nullable: true),
                    officeid = table.Column<string>(nullable: true),
                    HasQuery = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.QueryID);
                });

            migrationBuilder.CreateTable(
                name: "QueryHistory",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    applicationRef = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    dateCreated = table.Column<string>(nullable: true),
                    tableName = table.Column<string>(nullable: true),
                    QueryCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryHistory", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queries");

            migrationBuilder.DropTable(
                name: "QueryHistory");
        }
    }
}
