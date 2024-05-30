using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteYearFromJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4793881d-5c10-41c7-83d4-af386afc413f"));

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "FirstSemester",
                table: "Journals");

            migrationBuilder.AddColumn<byte>(
                name: "Semester",
                table: "Journals",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("c3a310e5-9389-49b8-a0f7-c76b810075fb"), null, "admin@admin", null, "Admin", "$2a$11$YJousOPglYozm7PrV0zJoeJyS1JF.HOZG9NFuC.KAGL6TJM6znEDO", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3a310e5-9389-49b8-a0f7-c76b810075fb"));

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Journals");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AcademicYear",
                table: "Journals",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<bool>(
                name: "FirstSemester",
                table: "Journals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("4793881d-5c10-41c7-83d4-af386afc413f"), null, "admin@admin", null, "Admin", "$2a$11$aBFhO0rYOgMaVQEk1/ZkX.ZZB6sIR3PM.l8XLP5NGMufbU5k24/BW", 0, null });
        }
    }
}
