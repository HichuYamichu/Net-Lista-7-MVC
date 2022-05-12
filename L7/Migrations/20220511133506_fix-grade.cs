using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class fixgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_GradeOptionId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "GradeOptionId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "GradeScaleId",
                table: "Grade",
                newName: "GradeOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GradeOptionsId",
                table: "Grade",
                column: "GradeOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionsId",
                table: "Grade",
                column: "GradeOptionsId",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionsId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_GradeOptionsId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "GradeOptionsId",
                table: "Grade",
                newName: "GradeScaleId");

            migrationBuilder.AddColumn<int>(
                name: "GradeOptionId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GradeOptionId",
                table: "Grade",
                column: "GradeOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                table: "Grade",
                column: "GradeOptionId",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
