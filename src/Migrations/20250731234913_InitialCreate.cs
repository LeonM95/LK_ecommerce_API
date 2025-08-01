using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    methodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK_category_Status",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    addressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.addressId);
                    table.ForeignKey(
                        name: "FK_Address_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shoppingCart",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartAddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cartUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCart", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_shoppingCart_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_shoppingCart_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Raiting = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_review_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_review_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    saleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paymentMethodId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.saleId);
                    table.ForeignKey(
                        name: "FK_Address",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "addressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_PaymentMethod_paymentMethodId",
                        column: x => x.paymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    cartProductsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    addedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => x.cartProductsId);
                    table.ForeignKey(
                        name: "FK_CartProducts_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProducts_shoppingCart_cartId",
                        column: x => x.cartId,
                        principalTable: "shoppingCart",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUCTSId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesDatails",
                columns: table => new
                {
                    SalesDatailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saleId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDatails", x => x.SalesDatailsId);
                    table.ForeignKey(
                        name: "FK_SalesDatails_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDatails_Sale_saleId",
                        column: x => x.saleId,
                        principalTable: "Sale",
                        principalColumn: "saleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_statusId",
                table: "Address",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_userId",
                table: "Address",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_cartId",
                table: "CartProducts",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_productId",
                table: "CartProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_statusId",
                table: "CartProducts",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_statusId",
                table: "Category",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_productId",
                table: "Image",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_statusId",
                table: "Image",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_statusId",
                table: "Products",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_review_productId",
                table: "review",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_review_UserId",
                table: "review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_AddressId",
                table: "Sale",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_paymentMethodId",
                table: "Sale",
                column: "paymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_productId",
                table: "Sale",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_statusId",
                table: "Sale",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDatails_productId",
                table: "SalesDatails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDatails_saleId",
                table: "SalesDatails",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCart_statusId",
                table: "shoppingCart",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCart_userId",
                table: "shoppingCart",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleId",
                table: "Users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_statusId",
                table: "Users",
                column: "statusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "SalesDatails");

            migrationBuilder.DropTable(
                name: "shoppingCart");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
