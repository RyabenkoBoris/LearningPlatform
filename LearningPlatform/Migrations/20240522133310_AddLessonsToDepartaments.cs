using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonsToDepartaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3a310e5-9389-49b8-a0f7-c76b810075fb"));

            migrationBuilder.CreateTable(
                name: "DepartmentEntityLessonEntity",
                columns: table => new
                {
                    DepartamentsId = table.Column<int>(type: "integer", nullable: false),
                    LessonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEntityLessonEntity", x => new { x.DepartamentsId, x.LessonsId });
                    table.ForeignKey(
                        name: "FK_DepartmentEntityLessonEntity_Departments_DepartamentsId",
                        column: x => x.DepartamentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentEntityLessonEntity_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("7ef91e9b-f57f-41bb-9980-037f23d25166"), null, "admin@admin", null, "Admin", "$2a$11$Kyhlq0aXr0paDXDB2QkGr.4RKni6PkbBGflZFShjmw.oMeddo49UC", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEntityLessonEntity_LessonsId",
                table: "DepartmentEntityLessonEntity",
                column: "LessonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentEntityLessonEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ef91e9b-f57f-41bb-9980-037f23d25166"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("c3a310e5-9389-49b8-a0f7-c76b810075fb"), null, "admin@admin", null, "Admin", "$2a$11$YJousOPglYozm7PrV0zJoeJyS1JF.HOZG9NFuC.KAGL6TJM6znEDO", 0, null });
        }
    }
}
