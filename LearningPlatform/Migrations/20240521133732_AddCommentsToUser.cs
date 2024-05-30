using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0baf0ff-5b39-49c8-8996-d8717257a76e"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("5e027436-1bad-4e59-8819-33533716ce8d"), null, "admin@admin", null, "Admin", "$2a$11$KcQtqxbX94.vAAl9KWadrey/U5lyYnphLqepWypR6GgNGE8ezOz/W", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e027436-1bad-4e59-8819-33533716ce8d"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d0baf0ff-5b39-49c8-8996-d8717257a76e"), null, "admin@admin", null, "Admin", "$2a$11$m7JDvWdpxvVJ4z37aYtcHekaDl4WXCZC2FnNS2QMO0nTyhcBL57aa", 0, null });
        }
    }
}
