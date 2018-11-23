using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meetupsApi.Migrations
{
    public partial class addConnpassDataColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "accepted",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "catchMesagge",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ended_at",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "event_type",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hash_tag",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "limit",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "owned_id",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "owned_nickname",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner_display_name",
                table: "ConnpassEventDataEntities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "started_at",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "waiting",
                table: "ConnpassEventDataEntities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "address",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "catchMesagge",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "ended_at",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "event_type",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "hash_tag",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "limit",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "owned_id",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "owned_nickname",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "owner_display_name",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "started_at",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "ConnpassEventDataEntities");

            migrationBuilder.DropColumn(
                name: "waiting",
                table: "ConnpassEventDataEntities");
        }
    }
}
