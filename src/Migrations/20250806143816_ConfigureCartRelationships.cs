using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCartRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_shoppingCart_ShoppingCartCartId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_ShoppingCartCartId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartCartId",
                table: "CartProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartCartId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ShoppingCartCartId",
                table: "CartProducts",
                column: "ShoppingCartCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_shoppingCart_ShoppingCartCartId",
                table: "CartProducts",
                column: "ShoppingCartCartId",
                principalTable: "shoppingCart",
                principalColumn: "cartId");
        }
    }
}
