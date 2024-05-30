using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTeachersJournalsLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntityTeacherEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ef91e9b-f57f-41bb-9980-037f23d25166"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("b957f332-7b2b-49aa-8eb3-dc548c4c7ea9"), null, "admin@admin", null, "Admin", "$2a$11$D8YmEBembDEchpO2JsxRR.djLo50btNK5ubk6jenHa6PzXSMaToqm", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b957f332-7b2b-49aa-8eb3-dc548c4c7ea9"));

            migrationBuilder.CreateTable(
                name: "JournalEntityTeacherEntity",
                columns: table => new
                {
                    JournalsId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntityTeacherEntity", x => new { x.JournalsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_JournalEntityTeacherEntity_Journals_JournalsId",
                        column: x => x.JournalsId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntityTeacherEntity_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("7ef91e9b-f57f-41bb-9980-037f23d25166"), null, "admin@admin", null, "Admin", "$2a$11$Kyhlq0aXr0paDXDB2QkGr.4RKni6PkbBGflZFShjmw.oMeddo49UC", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntityTeacherEntity_TeachersId",
                table: "JournalEntityTeacherEntity",
                column: "TeachersId");
        }
    }
}
