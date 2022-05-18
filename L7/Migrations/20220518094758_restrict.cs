using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class restrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                schema: "Identity",
                table: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                schema: "Identity",
                table: "Grade",
                column: "GradeOptionId",
                principalSchema: "Identity",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                schema: "Identity",
                table: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                schema: "Identity",
                table: "Grade",
                column: "GradeOptionId",
                principalSchema: "Identity",
                principalTable: "GradeOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
