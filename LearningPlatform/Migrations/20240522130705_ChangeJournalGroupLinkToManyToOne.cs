using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeJournalGroupLinkToManyToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupEntityJournalEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cd93548d-7d4a-4cce-a22f-e03577688918"));

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Journals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("4793881d-5c10-41c7-83d4-af386afc413f"), null, "admin@admin", null, "Admin", "$2a$11$aBFhO0rYOgMaVQEk1/ZkX.ZZB6sIR3PM.l8XLP5NGMufbU5k24/BW", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Journals_GroupId",
                table: "Journals",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Groups_GroupId",
                table: "Journals",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Groups_GroupId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_GroupId",
                table: "Journals");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4793881d-5c10-41c7-83d4-af386afc413f"));

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Journals");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("cd93548d-7d4a-4cce-a22f-e03577688918"), null, "admin@admin", null, "Admin", "$2a$11$jzBvNowGVpaDJQaGNhtLOe2nA/WmckxK.nv6cfHGCB96JFcMqhDfu", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntityJournalEntity_JournalsId",
                table: "GroupEntityJournalEntity",
                column: "JournalsId");
        }
    }
}
