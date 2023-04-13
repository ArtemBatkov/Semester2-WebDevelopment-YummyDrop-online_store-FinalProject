using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyDrop_online_store.Migrations
{
    /// <inheritdoc />
    public partial class AddYummyItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YummyItem_FruitBoxTable_FruitBoxId",
                table: "YummyItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YummyItem",
                table: "YummyItem");

            migrationBuilder.RenameTable(
                name: "YummyItem",
                newName: "YummyItemTable");

            migrationBuilder.RenameIndex(
                name: "IX_YummyItem_FruitBoxId",
                table: "YummyItemTable",
                newName: "IX_YummyItemTable_FruitBoxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YummyItemTable",
                table: "YummyItemTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_YummyItemTable_FruitBoxTable_FruitBoxId",
                table: "YummyItemTable",
                column: "FruitBoxId",
                principalTable: "FruitBoxTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YummyItemTable_FruitBoxTable_FruitBoxId",
                table: "YummyItemTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YummyItemTable",
                table: "YummyItemTable");

            migrationBuilder.RenameTable(
                name: "YummyItemTable",
                newName: "YummyItem");

            migrationBuilder.RenameIndex(
                name: "IX_YummyItemTable_FruitBoxId",
                table: "YummyItem",
                newName: "IX_YummyItem_FruitBoxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YummyItem",
                table: "YummyItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_YummyItem_FruitBoxTable_FruitBoxId",
                table: "YummyItem",
                column: "FruitBoxId",
                principalTable: "FruitBoxTable",
                principalColumn: "Id");
        }
    }
}
