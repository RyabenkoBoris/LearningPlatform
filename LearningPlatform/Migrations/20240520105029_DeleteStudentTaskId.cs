using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStudentTaskId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79651912-e3f9-4027-aa85-de0694f49ea9"));

            migrationBuilder.DropColumn(
                name: "StudentTaskId",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d1f0d79c-9bfb-4d3d-859e-097aec32d378"), null, "admin@admin", null, "Admin", "$2a$11$p64US29i1UG48eaokm91vemkcGdq.sz9gxZcI8J4rkjRAUB/lXjJK", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d1f0d79c-9bfb-4d3d-859e-097aec32d378"));

            migrationBuilder.AddColumn<int>(
                name: "StudentTaskId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("79651912-e3f9-4027-aa85-de0694f49ea9"), null, "admin@admin", null, "Admin", "$2a$11$F.iTzGGWRRIMD6hB/vMYB.w0KLCcdqXV6UG20k/qzVGoyCK7kcl4S", 0, null });
        }
    }
}
