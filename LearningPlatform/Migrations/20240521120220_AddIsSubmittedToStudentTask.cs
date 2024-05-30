using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSubmittedToStudentTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e3ec6270-0dab-4d1c-b3e6-5b06b38502f8"));

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "StudentTasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d0baf0ff-5b39-49c8-8996-d8717257a76e"), null, "admin@admin", null, "Admin", "$2a$11$m7JDvWdpxvVJ4z37aYtcHekaDl4WXCZC2FnNS2QMO0nTyhcBL57aa", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0baf0ff-5b39-49c8-8996-d8717257a76e"));

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "StudentTasks");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("e3ec6270-0dab-4d1c-b3e6-5b06b38502f8"), null, "admin@admin", null, "Admin", "$2a$11$uQbDppz.VQOKnHc5NEeL.eGlcw6cHCCOM6styQ3CsJZFtIWAvZ40q", 0, null });
        }
    }
}
