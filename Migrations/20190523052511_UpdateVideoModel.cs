using Microsoft.EntityFrameworkCore.Migrations;

namespace dat_q_ngo.Migrations
{
    public partial class UpdateVideoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbUpCounts",
                table: "Videos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ThumbUpCounts",
                table: "Videos");
        }
    }
}
