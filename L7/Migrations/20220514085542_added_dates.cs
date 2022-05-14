using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L7.Migrations
{
    public partial class added_dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeOption_Value",
                table: "GradeOption",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_ClassificationId",
                table: "Grade",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                table: "Grade",
                column: "ClassificationId",
                principalTable: "Classification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Classification_ClassificationId",
                table: "Grade");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropIndex(
                name: "IX_GradeOption_Value",
                table: "GradeOption");

            migrationBuilder.DropIndex(
                name: "IX_Grade_ClassificationId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "Grade");
        }
    }
}
