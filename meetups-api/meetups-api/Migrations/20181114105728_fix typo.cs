using Microsoft.EntityFrameworkCore.Migrations;

namespace meetupsApi.Migrations
{
    public partial class fixtypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RventDescription",
                table: "ConnpassEventDataEntities",
                newName: "EventDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDescription",
                table: "ConnpassEventDataEntities",
                newName: "RventDescription");
        }
    }
}
