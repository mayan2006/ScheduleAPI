using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class buildAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "studentsList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teachers_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_studentsList_ClassId",
                table: "studentsList",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_ClassId",
                table: "teachers",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentsList_classes_ClassId",
                table: "studentsList",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsList_classes_ClassId",
                table: "studentsList");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropIndex(
                name: "IX_studentsList_ClassId",
                table: "studentsList");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "studentsList");
        }
    }
}
