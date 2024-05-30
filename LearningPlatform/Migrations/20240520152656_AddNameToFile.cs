using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ae05e92-0502-4d08-a557-3f9325c3c9d4"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FilePaths",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("4fd81173-61c8-4ba9-b0b2-2aa42381b140"), null, "admin@admin", null, "Admin", "$2a$11$cz12qsGtTEWjB75252WYrecjYfpX4VUyUNBTmt.TQJP.9oXJWu8we", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4fd81173-61c8-4ba9-b0b2-2aa42381b140"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FilePaths");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("2ae05e92-0502-4d08-a557-3f9325c3c9d4"), null, "admin@admin", null, "Admin", "$2a$11$EnuVpgDMj/k9vtriZVZjZ.ECSe4Wk42pr6/jT5GPNzKsKzjTZtm4G", 0, null });
        }
    }
}
