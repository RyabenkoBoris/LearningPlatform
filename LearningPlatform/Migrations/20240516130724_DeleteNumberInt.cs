using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNumberInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5394a15b-c71e-4f70-87f8-0d1cbf366dd6"));

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("ab3eface-dfc5-44ba-bffe-1579869b0134"), null, "admin@admin", null, "Admin", "$2a$11$23EzkGVgfrauLpSCTuSPsO2WTrG32DbzZ3qSVQeLf9fM8C2XwO6ri", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ab3eface-dfc5-44ba-bffe-1579869b0134"));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "Number", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("5394a15b-c71e-4f70-87f8-0d1cbf366dd6"), null, "admin@admin", null, "Admin", null, "$2a$11$1wwYNoiPQ8E8gBqDv1ZRz.L6EysFvMJcmuEqJaPfcdfOn9146JjBO", 0, null });
        }
    }
}
