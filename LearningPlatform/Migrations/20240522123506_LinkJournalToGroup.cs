using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class LinkJournalToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntityUserEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ac16d9b-a1a5-4d5d-b6b4-2a9a126aef85"));

            migrationBuilder.CreateTable(
                name: "GroupEntityJournalEntity",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    JournalsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntityJournalEntity", x => new { x.GroupsId, x.JournalsId });
                    table.ForeignKey(
                        name: "FK_GroupEntityJournalEntity_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEntityJournalEntity_Journals_JournalsId",
                        column: x => x.JournalsId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                values: new object[] { new Guid("cd93548d-7d4a-4cce-a22f-e03577688918"), null, "admin@admin", null, "Admin", "$2a$11$jzBvNowGVpaDJQaGNhtLOe2nA/WmckxK.nv6cfHGCB96JFcMqhDfu", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntityJournalEntity_JournalsId",
                table: "GroupEntityJournalEntity",
                column: "JournalsId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntityTeacherEntity_TeachersId",
                table: "JournalEntityTeacherEntity",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupEntityJournalEntity");

            migrationBuilder.DropTable(
                name: "JournalEntityTeacherEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cd93548d-7d4a-4cce-a22f-e03577688918"));

            migrationBuilder.CreateTable(
                name: "JournalEntityUserEntity",
                columns: table => new
                {
                    JournalsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntityUserEntity", x => new { x.JournalsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_JournalEntityUserEntity_Journals_JournalsId",
                        column: x => x.JournalsId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("8ac16d9b-a1a5-4d5d-b6b4-2a9a126aef85"), null, "admin@admin", null, "Admin", "$2a$11$lpw752Nolo3KbveGGcdUbeRJ7BsvFQ4370WJyU4K6ZtftnyHVUuH6", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntityUserEntity_UsersId",
                table: "JournalEntityUserEntity",
                column: "UsersId");
        }
    }
}
