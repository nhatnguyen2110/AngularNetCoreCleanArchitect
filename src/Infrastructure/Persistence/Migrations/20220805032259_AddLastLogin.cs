using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class AddLastLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Accounts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Accounts");
        }
    }
}
