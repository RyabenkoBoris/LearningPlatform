using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTaskStudentTaskLinkToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTasks");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdd3366-a264-4a88-8cc3-bba214c75df0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("79651912-e3f9-4027-aa85-de0694f49ea9"), null, "admin@admin", null, "Admin", "$2a$11$F.iTzGGWRRIMD6hB/vMYB.w0KLCcdqXV6UG20k/qzVGoyCK7kcl4S", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTasks",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTasks");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79651912-e3f9-4027-aa85-de0694f49ea9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("efdd3366-a264-4a88-8cc3-bba214c75df0"), null, "admin@admin", null, "Admin", "$2a$11$H2I5Gu7fx5bP3k7zLlBO9et.3U0.5PGtWYobMxR11CnEt1Qa0Bjda", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTasks",
                column: "TaskId",
                unique: true);
        }
    }
}
