using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleAndCartRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Products_productId",
                table: "Sale");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Sale",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_productId",
                table: "Sale",
                newName: "IX_Sale_UserId");

            migrationBuilder.AddColumn<int>(
                name: "SaleId1",
                table: "SalesDatails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartCartId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDatails_SaleId1",
                table: "SalesDatails",
                column: "SaleId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Users_UserId",
                table: "Sale",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDatails_Sale_SaleId1",
                table: "SalesDatails",
                column: "SaleId1",
                principalTable: "Sale",
                principalColumn: "saleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_shoppingCart_ShoppingCartCartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Users_UserId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDatails_Sale_SaleId1",
                table: "SalesDatails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDatails_SaleId1",
                table: "SalesDatails");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_ShoppingCartCartId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "SaleId1",
                table: "SalesDatails");

            migrationBuilder.DropColumn(
                name: "ShoppingCartCartId",
                table: "CartProducts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sale",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_UserId",
                table: "Sale",
                newName: "IX_Sale_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Products_productId",
                table: "Sale",
                column: "productId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
