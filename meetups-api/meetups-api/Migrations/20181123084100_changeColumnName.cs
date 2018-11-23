using Microsoft.EntityFrameworkCore.Migrations;

namespace meetupsApi.Migrations
{
    public partial class changeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventUrl",
                table: "ConnpassEventDataEntities",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "EventTitle",
                table: "ConnpassEventDataEntities",
                newName: "event_url");

            migrationBuilder.RenameColumn(
                name: "EventDescription",
                table: "ConnpassEventDataEntities",
                newName: "description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "ConnpassEventDataEntities",
                newName: "EventUrl");

            migrationBuilder.RenameColumn(
                name: "event_url",
                table: "ConnpassEventDataEntities",
                newName: "EventTitle");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ConnpassEventDataEntities",
                newName: "EventDescription");
        }
    }
}
