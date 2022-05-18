using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class unique_enollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentId",
                schema: "Identity",
                table: "Enrollment");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId_CourseId",
                schema: "Identity",
                table: "Enrollment",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentId_CourseId",
                schema: "Identity",
                table: "Enrollment");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId",
                schema: "Identity",
                table: "Enrollment",
                column: "StudentId");
        }
    }
}
