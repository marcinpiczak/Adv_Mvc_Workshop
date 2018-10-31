using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyMessagePortal.Migrations
{
    public partial class Remove_channel_color_requirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChannelColor",
                schema: "dbo",
                table: "Channels",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChannelColor",
                schema: "dbo",
                table: "Channels",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6,
                oldNullable: true);
        }
    }
}
