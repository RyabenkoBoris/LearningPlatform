using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddTextToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d1f0d79c-9bfb-4d3d-859e-097aec32d378"));

            migrationBuilder.DropColumn(
                name: "Date",
                table: "StudentTasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskText",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "StudentTasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("804101dd-6316-4021-a25b-3cb681ba290c"), null, "admin@admin", null, "Admin", "$2a$11$a4hwAK1eYY1qh4Tn9tJU9.rJzAsWXIVeY9/oYcbyB2GMhwcUSxO8m", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("804101dd-6316-4021-a25b-3cb681ba290c"));

            migrationBuilder.DropColumn(
                name: "TaskText",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "StudentTasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "StudentTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d1f0d79c-9bfb-4d3d-859e-097aec32d378"), null, "admin@admin", null, "Admin", "$2a$11$p64US29i1UG48eaokm91vemkcGdq.sz9gxZcI8J4rkjRAUB/lXjJK", 0, null });
        }
    }
}
