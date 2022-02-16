using Microsoft.EntityFrameworkCore.Migrations;

namespace P512FiorelloBack.Migrations
{
    public partial class createdFlowersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainImage = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DiscountPrice = table.Column<double>(nullable: true),
                    Desc = table.Column<string>(maxLength: 500, nullable: false),
                    SKUCode = table.Column<string>(maxLength: 18, nullable: false),
                    Weight = table.Column<string>(maxLength: 10, nullable: true),
                    Dimension = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerCategories_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerCategories_CategoryId",
                table: "FlowerCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerCategories_FlowerId",
                table: "FlowerCategories",
                column: "FlowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerCategories");

            migrationBuilder.DropTable(
                name: "Flowers");
        }
    }
}
