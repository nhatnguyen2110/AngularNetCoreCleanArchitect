using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class UpdateRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropTable(
                name: "CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.CreateTable(
                name: "CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "CleanArchitecture.Application.Common.Interfaces.IApplicationDbContext.Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
