using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupsToTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69178df2-f757-45cc-80aa-be82f3da1c61"));

            migrationBuilder.CreateTable(
                name: "GroupEntityTeacherEntity",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntityTeacherEntity", x => new { x.GroupsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_GroupEntityTeacherEntity_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEntityTeacherEntity_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("f82788a6-8c56-45d6-8220-c387938e0879"), null, "admin@admin", null, "Admin", "$2a$11$FtoLqAnzEGW63rw1H2eFpu1AiLvmBmD3UYDENXl5cjxPiX4ogngIm", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntityTeacherEntity_TeachersId",
                table: "GroupEntityTeacherEntity",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupEntityTeacherEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f82788a6-8c56-45d6-8220-c387938e0879"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("69178df2-f757-45cc-80aa-be82f3da1c61"), null, "admin@admin", null, "Admin", "$2a$11$Q34M4TiwP/N4O/zelsYviOM0INIgvBqGB2/3BB7nzfIf3.0M9jHGK", 0, null });
        }
    }
}
