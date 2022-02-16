using Microsoft.EntityFrameworkCore.Migrations;

namespace P512FiorelloBack.Migrations
{
    public partial class updateTitleSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Sliders",
                maxLength: 170,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Sliders",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 170);
        }
    }
}
