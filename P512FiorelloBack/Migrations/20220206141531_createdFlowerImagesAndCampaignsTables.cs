using Microsoft.EntityFrameworkCore.Migrations;

namespace P512FiorelloBack.Migrations
{
    public partial class createdFlowerImagesAndCampaignsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Flowers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercent = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FlowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerImages_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_CampaignId",
                table: "Flowers",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerImages_FlowerId",
                table: "FlowerImages",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Campaigns_CampaignId",
                table: "Flowers",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Campaigns_CampaignId",
                table: "Flowers");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "FlowerImages");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_CampaignId",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Flowers");
        }
    }
}
