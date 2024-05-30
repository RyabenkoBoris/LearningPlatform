using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddMarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5083187f-dd9a-4962-a4b6-ad79ce4bd8a0"));

            migrationBuilder.AddColumn<short>(
                name: "MaxMark",
                table: "Tasks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Mark",
                table: "StudentTasks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("8ac16d9b-a1a5-4d5d-b6b4-2a9a126aef85"), null, "admin@admin", null, "Admin", "$2a$11$lpw752Nolo3KbveGGcdUbeRJ7BsvFQ4370WJyU4K6ZtftnyHVUuH6", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ac16d9b-a1a5-4d5d-b6b4-2a9a126aef85"));

            migrationBuilder.DropColumn(
                name: "MaxMark",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "StudentTasks");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("5083187f-dd9a-4962-a4b6-ad79ce4bd8a0"), null, "admin@admin", null, "Admin", "$2a$11$KClNVV.cpEiNSrBpcp/zNuVqF93v/nX9lgLCN3jvZi6g9Q.YXPB.u", 0, null });
        }
    }
}
