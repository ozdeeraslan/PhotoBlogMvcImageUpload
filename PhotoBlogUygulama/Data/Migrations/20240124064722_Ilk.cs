using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoBlogUygulama.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gonderiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResimYolu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gonderiler", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Gonderiler",
                columns: new[] { "Id", "Baslik", "ResimYolu" },
                values: new object[,]
                {
                    { 1, "Denizin kokusu...", "deniz.jpg" },
                    { 2, "Manzarayi izliyorum gözlerim kapali...", "manzara.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gonderiler");
        }
    }
}
