using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelephoneDirectory.Data.Access.Migrations
{
    public partial class addreporttabledetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportFilName",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportPath",
                table: "Reports",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportFilName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportPath",
                table: "Reports");
        }
    }
}
