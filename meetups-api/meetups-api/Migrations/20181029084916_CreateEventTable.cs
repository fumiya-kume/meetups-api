using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meetupsApi.Migrations
{
    public partial class CreateEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    url = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: true)
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
                    event_url = table.Column<string>(nullable: true),
                    event_type = table.Column<string>(nullable: true),
                    owner_nickname = table.Column<string>(nullable: true),
                    seriesid = table.Column<int>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    lat = table.Column<string>(nullable: true),
                    started_at = table.Column<DateTime>(nullable: false),
                    hash_tag = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    lon = table.Column<string>(nullable: true),
                    waiting = table.Column<int>(nullable: false),
                    limit = table.Column<int>(nullable: false),
                    owner_id = table.Column<int>(nullable: false),
                    owner_display_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    _catch = table.Column<string>(nullable: true),
                    accepted = table.Column<int>(nullable: false),
                    ended_at = table.Column<DateTime>(nullable: false),
                    place = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
