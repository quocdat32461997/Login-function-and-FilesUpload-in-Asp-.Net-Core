using Microsoft.EntityFrameworkCore.Migrations;

namespace dat_q_ngo.Migrations
{
    public partial class VideoMdoelUserModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserModelId",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Videos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ApplicationUserModelId",
                table: "Videos",
                column: "ApplicationUserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_ApplicationUserModelId",
                table: "Videos",
                column: "ApplicationUserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_ApplicationUserModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ApplicationUserModelId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserModelId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Videos");
        }
    }
}
