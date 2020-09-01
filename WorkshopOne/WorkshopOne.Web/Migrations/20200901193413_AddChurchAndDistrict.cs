using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkshopOne.Web.Migrations
{
    public partial class AddChurchAndDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "countrysides",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountrysideId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_districts_countrysides_CountrysideId",
                        column: x => x.CountrysideId,
                        principalTable: "countrysides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "churches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DistrictId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_churches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_churches_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_churches_DistrictId",
                table: "churches",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_churches_Name",
                table: "churches",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_districts_CountrysideId",
                table: "districts",
                column: "CountrysideId");

            migrationBuilder.CreateIndex(
                name: "IX_districts_Name",
                table: "districts",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "churches");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "countrysides",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
