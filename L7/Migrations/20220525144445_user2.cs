using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class user2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Admin_AdminId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                schema: "Identity",
                table: "ApplicationUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "InstructorId",
                unique: true,
                filter: "[InstructorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Admin_AdminId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "AdminId",
                principalSchema: "Identity",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "InstructorId",
                principalSchema: "Identity",
                principalTable: "Instructor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Admin_AdminId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Instructor_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_InstructorId",
                schema: "Identity",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                schema: "Identity",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_InstructorId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "InstructorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Admin_AdminId",
                schema: "Identity",
                table: "ApplicationUser",
                column: "AdminId",
                principalSchema: "Identity",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
