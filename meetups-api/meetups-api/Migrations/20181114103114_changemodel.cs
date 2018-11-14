using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meetupsApi.Migrations
{
    public partial class changemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.CreateTable(
                name: "ConnpassEventDataEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventTitle = table.Column<string>(nullable: true),
                    RventDescription = table.Column<string>(nullable: true),
                    EventUrl = table.Column<string>(nullable: true),
                    Lon = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnpassEventDataEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnpassEventDataEntities");

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    event_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    _catch = table.Column<string>(nullable: true),
                    accepted = table.Column<int>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    ended_at = table.Column<DateTime>(nullable: false),
                    event_type = table.Column<string>(nullable: true),
                    event_url = table.Column<string>(nullable: true),
                    hash_tag = table.Column<string>(nullable: true),
                    lat = table.Column<string>(nullable: true),
                    limit = table.Column<int>(nullable: false),
                    lon = table.Column<string>(nullable: true),
                    owner_display_name = table.Column<string>(nullable: true),
                    owner_id = table.Column<int>(nullable: false),
                    owner_nickname = table.Column<string>(nullable: true),
                    place = table.Column<string>(nullable: true),
                    seriesid = table.Column<int>(nullable: true),
                    started_at = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    waiting = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_Event_Series_seriesid",
                        column: x => x.seriesid,
                        principalTable: "Series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_seriesid",
                table: "Event",
                column: "seriesid");
        }
    }
}
