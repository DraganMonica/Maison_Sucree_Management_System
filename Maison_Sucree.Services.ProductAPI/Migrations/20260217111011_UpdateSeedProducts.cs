using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maison_Sucree.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "Description",
                value: "Mini prăjituri rafinate cu straturi delicate de mousse de căpșuni și cremă fină de vanilie, decorate elegant pentru un aspect premium.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "Description",
                value: "Mini prăjituri rafinate cu straturi delicate de mousse de căpșuni și cremă fină de vanilie, decorate elegant pentru un aspect premium. Desert perfect pentru evenimente speciale sau momente dulci sofisticate.");
        }
    }
}
