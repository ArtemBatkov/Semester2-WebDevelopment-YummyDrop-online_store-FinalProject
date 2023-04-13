using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyDrop_online_store.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FruitBoxTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitBoxTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YummyItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropChance = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FruitBoxId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YummyItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YummyItem_FruitBoxTable_FruitBoxId",
                        column: x => x.FruitBoxId,
                        principalTable: "FruitBoxTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_YummyItem_FruitBoxId",
                table: "YummyItem",
                column: "FruitBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YummyItem");

            migrationBuilder.DropTable(
                name: "FruitBoxTable");
        }
    }
}
