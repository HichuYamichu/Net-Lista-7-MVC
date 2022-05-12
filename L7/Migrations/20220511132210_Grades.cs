using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class Grades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Enrollment");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Grade",
                newName: "GradeScaleId");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeOptionId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GradeOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeOption", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_EnrollmentId",
                table: "Grade",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GradeOptionId",
                table: "Grade",
                column: "GradeOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Enrollment_EnrollmentId",
                table: "Grade",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Grade_Enrollment_EnrollmentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_GradeOption_GradeOptionId",
                table: "Grade");

            migrationBuilder.DropTable(
                name: "GradeOption");

            migrationBuilder.DropIndex(
                name: "IX_Grade_EnrollmentId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_GradeOptionId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "GradeOptionId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "GradeScaleId",
                table: "Grade",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
