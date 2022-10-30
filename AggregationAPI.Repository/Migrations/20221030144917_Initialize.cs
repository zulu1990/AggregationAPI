using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AggretationApp.Repository.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TINKLAS = table.Column<string>(nullable: true),
                    OBT_PAVADINIMAS = table.Column<string>(nullable: true),
                    OBJ_GV_TIPAS = table.Column<string>(nullable: true),
                    OBJ_NUMERIS = table.Column<int>(nullable: false),
                    P_PLUS = table.Column<double>(nullable: true),
                    PL_T = table.Column<DateTime>(nullable: false),
                    P_MINUS = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
