using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class restrict4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "InstructorId",
                principalSchema: "Identity",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "InstructorId",
                principalSchema: "Identity",
                principalTable: "Instructor",
                principalColumn: "Id");
        }
    }
}
