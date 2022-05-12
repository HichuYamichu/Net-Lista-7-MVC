using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class fixgrade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionsId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "GradeOptionsId",
                table: "Grade",
                newName: "GradeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_GradeOptionsId",
                table: "Grade",
                newName: "IX_Grade_GradeOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                table: "Grade",
                column: "GradeOptionId",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "GradeOptionId",
                table: "Grade",
                newName: "GradeOptionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_GradeOptionId",
                table: "Grade",
                newName: "IX_Grade_GradeOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionsId",
                table: "Grade",
                column: "GradeOptionsId",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
