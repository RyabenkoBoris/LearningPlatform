using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e027436-1bad-4e59-8819-33533716ce8d"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("5083187f-dd9a-4962-a4b6-ad79ce4bd8a0"), null, "admin@admin", null, "Admin", "$2a$11$KClNVV.cpEiNSrBpcp/zNuVqF93v/nX9lgLCN3jvZi6g9Q.YXPB.u", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5083187f-dd9a-4962-a4b6-ad79ce4bd8a0"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("5e027436-1bad-4e59-8819-33533716ce8d"), null, "admin@admin", null, "Admin", "$2a$11$KcQtqxbX94.vAAl9KWadrey/U5lyYnphLqepWypR6GgNGE8ezOz/W", 0, null });
        }
    }
}
