using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maison_Sucree.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Chocolate", "Ou artizanal din ciocolată fină, umplut cu cremă catifelată de ciocolată și decorat cu căpșuni proaspete și trufe crocante. Desert premium perfect pentru ocazii speciale.", null, "/images/products/strawberry_chocolate_egg.png", "Strawberry Chocolate Egg", 12.0 },
                    { 2, "Cake", "Mini prăjituri Red Velvet cu straturi delicate de blat catifelat și cremă fină de vanilie, decorate cu fructe proaspete. Un desert elegant, perfect pentru momente speciale.", null, "/images/products/red_velvet.png", "Red Velvet Cream Petits", 13.5 },
                    { 3, "Candy", "Mini delicii din ciocolată fină, umplute cu cremă catifelată de vanilie și decorate elegant cu ganache de ciocolată. O combinație rafinată între dulceața vaniliei și intensitatea cacao-ului.", null, "/images/products/vanilla_chocolate.png", "Vanilla Cream Chocolate Petits", 7.9000000000000004 },
                    { 4, "Cake", "Éclair delicat umplut cu cremă fină de caramel și acoperit cu glazură catifelată și crumble crocant. Un desert rafinat inspirat din patiseria franțuzească clasică.", null, "/images/products/eclair.png", "Caramel Éclair Royale", 11.9 },
                    { 5, "Cake", "Mini prăjituri rafinate cu straturi delicate de mousse de căpșuni și cremă fină de vanilie, decorate elegant pentru un aspect premium.", null, "/images/products/strawberry_vanilla_squares.png", "Strawberry Vanilla Petit Squares", 10.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
