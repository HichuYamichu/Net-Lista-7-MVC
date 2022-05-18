using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class restrict2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Subject_SubjectId",
                schema: "Identity",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                schema: "Identity",
                table: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Subject_SubjectId",
                schema: "Identity",
                table: "Course",
                column: "SubjectId",
                principalSchema: "Identity",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                schema: "Identity",
                table: "Grade",
                column: "ClassificationId",
                principalSchema: "Identity",
                principalTable: "Classification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Subject_SubjectId",
                schema: "Identity",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                schema: "Identity",
                table: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Subject_SubjectId",
                schema: "Identity",
                table: "Course",
                column: "SubjectId",
                principalSchema: "Identity",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                schema: "Identity",
                table: "Grade",
                column: "ClassificationId",
                principalSchema: "Identity",
                principalTable: "Classification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
