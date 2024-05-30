using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UserChangeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f29cd9da-7d8a-4108-90dc-356f47bbf611"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Users",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartamentId", "Email", "GroupId", "Name", "NumberId", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("a30fe03f-2a8c-43e9-aaa6-4ca8321241c7"), null, "admin@admin", null, "Admin", null, "$2a$11$d6x.xeLkp6DjkEjPdQuXnew2m.fu3hyT.G6j60VjXRElrp56I/vXu", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a30fe03f-2a8c-43e9-aaa6-4ca8321241c7"));

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartamentId", "Email", "FirstName", "GroupId", "LastName", "MiddleName", "NumberId", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("f29cd9da-7d8a-4108-90dc-356f47bbf611"), null, "admin@admin", "Admin", null, "", "", null, "$2a$11$cUff033QxIj8OkkCCY.P4.QeFxp1iKaQobCJf0JNcMJw5qVUwDxvK", 0, null });
        }
    }
}
