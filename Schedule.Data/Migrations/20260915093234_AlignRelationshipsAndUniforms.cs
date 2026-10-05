using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Data.Migrations
{
    public partial class AlignRelationshipsAndUniforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsList_classes_ClassId",
                table: "studentsList");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_classes_ClassId",
                table: "teachers");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropColumn(
                name: "StudentClass",
                table: "studentsList");

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClassTeacher",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTeacher", x => new { x.ClassesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_ClassTeacher_classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uniforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uniforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_uniforms_studentsList_StudentId",
                        column: x => x.StudentId,
                        principalTable: "studentsList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_TeachersId",
                table: "ClassTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_uniforms_StudentId",
                table: "uniforms",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_studentsList_classes_ClassId",
                table: "studentsList",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsList_classes_ClassId",
                table: "studentsList");

            migrationBuilder.DropTable(
                name: "ClassTeacher");

            migrationBuilder.DropTable(
                name: "uniforms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "classes");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "teachers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentClass",
                table: "studentsList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_classes_ClassId",
                table: "teachers",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id");
        }
    }
}
