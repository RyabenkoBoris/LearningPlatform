using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class InsertSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d379d82c-94a2-4687-be2a-488ca4a36630"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("cee053f3-d9bf-4e65-bbcf-9464fef7943c"), null, "admin@admin", null, "Admin", "$2a$11$giyWPLTk8fHHqXeahIOpMe0ME3Wl3KigwnUnlhZTI5zmBFfXSnqiy", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cee053f3-d9bf-4e65-bbcf-9464fef7943c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d379d82c-94a2-4687-be2a-488ca4a36630"), null, "admin", null, "Admin", "$2a$11$ke3AbyC5iNjIvVP.tDFaDujl2/mOZe7cTRWVZIYbkLk7zigE.Ysae", 0, null });
        }
    }
}
