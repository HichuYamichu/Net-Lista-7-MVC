using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class restrict3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Instructor_InstructorId",
                schema: "Identity",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Instructor_InstructorId",
                schema: "Identity",
                table: "Course",
                column: "InstructorId",
                principalSchema: "Identity",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Instructor_InstructorId",
                schema: "Identity",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Instructor_InstructorId",
                schema: "Identity",
                table: "Course",
                column: "InstructorId",
                principalSchema: "Identity",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
