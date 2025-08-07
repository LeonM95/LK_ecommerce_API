using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_review_StatusId",
                table: "review",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_review_Status_StatusId",
                table: "review",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_review_Status_StatusId",
                table: "review");

            migrationBuilder.DropIndex(
                name: "IX_review_StatusId",
                table: "review");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "review");
        }
    }
}
