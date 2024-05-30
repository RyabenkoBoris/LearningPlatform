using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddMainUserToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f78d5af-b77b-4535-a55c-9c019f428b38"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Courses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("efdd3366-a264-4a88-8cc3-bba214c75df0"), null, "admin@admin", null, "Admin", "$2a$11$H2I5Gu7fx5bP3k7zLlBO9et.3U0.5PGtWYobMxR11CnEt1Qa0Bjda", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efdd3366-a264-4a88-8cc3-bba214c75df0"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("1f78d5af-b77b-4535-a55c-9c019f428b38"), null, "admin@admin", null, "Admin", "$2a$11$gmNsIAGQ4L12dRRhozgHIOPw4F0SOs7seSModOtysMfObkvuPcDMi", 0, null });
        }
    }
}
