using Microsoft.EntityFrameworkCore.Migrations;

namespace BookAPI.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cars_CarId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CarId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Books",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CarId",
                table: "Books",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cars_CarId",
                table: "Books",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cars_CarId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CarId",
                table: "Books");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarId",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CarId1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CarId1",
                table: "Books",
                column: "CarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cars_CarId1",
                table: "Books",
                column: "CarId1",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
