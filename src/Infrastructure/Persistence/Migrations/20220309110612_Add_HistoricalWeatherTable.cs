using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class Add_HistoricalWeatherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricalWeatherDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Dt = table.Column<double>(type: "float", nullable: false),
                    Sunrise = table.Column<double>(type: "float", nullable: false),
                    Sunset = table.Column<double>(type: "float", nullable: false),
                    Temp_avg = table.Column<double>(type: "float", nullable: false),
                    Temp_min = table.Column<double>(type: "float", nullable: false),
                    Temp_max = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Dew_point = table.Column<double>(type: "float", nullable: false),
                    Pop = table.Column<double>(type: "float", nullable: false),
                    Wind_speed = table.Column<double>(type: "float", nullable: false),
                    Clouds = table.Column<double>(type: "float", nullable: false),
                    Weather_id = table.Column<int>(type: "int", nullable: false),
                    Weather_main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weather_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weather_icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temp_morn = table.Column<double>(type: "float", nullable: false),
                    Temp_day = table.Column<double>(type: "float", nullable: false),
                    Temp_eve = table.Column<double>(type: "float", nullable: false),
                    Temp_night = table.Column<double>(type: "float", nullable: false),
                    WeatherId_morn = table.Column<int>(type: "int", nullable: false),
                    WeatherMain_morn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherDesc_morn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherIcon_morn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherId_day = table.Column<int>(type: "int", nullable: false),
                    WeatherMain_day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherDesc_day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherIcon_day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherId_eve = table.Column<int>(type: "int", nullable: false),
                    WeatherMain_eve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherDesc_eve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherIcon_eve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherId_night = table.Column<int>(type: "int", nullable: false),
                    WeatherMain_night = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherDesc_night = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherIcon_night = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalWeatherDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalWeatherDatas_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalWeatherDatas_ProvinceId",
                table: "HistoricalWeatherDatas",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalWeatherDatas");
        }
    }
}
