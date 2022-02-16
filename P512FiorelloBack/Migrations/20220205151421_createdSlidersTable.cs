using Microsoft.EntityFrameworkCore.Migrations;

namespace P512FiorelloBack.Migrations
{
    public partial class createdSlidersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 300, nullable: false),
                    Desc = table.Column<string>(maxLength: 300, nullable: false),
                    Title = table.Column<string>(maxLength: 70, nullable: false),
                    SignatureImage = table.Column<string>(maxLength: 300, nullable: true),
                    LeftIcon = table.Column<string>(maxLength: 100, nullable: false),
                    RightIcon = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
