using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateOnlyToDateTimeInGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87438678-fb1e-4118-b378-bd2c20ac0f8b"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Grades",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("d379d82c-94a2-4687-be2a-488ca4a36630"), null, "admin@admin", null, "Admin", "$2a$11$ke3AbyC5iNjIvVP.tDFaDujl2/mOZe7cTRWVZIYbkLk7zigE.Ysae", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d379d82c-94a2-4687-be2a-488ca4a36630"));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Grades",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("87438678-fb1e-4118-b378-bd2c20ac0f8b"), null, "admin@admin", null, "Admin", "$2a$11$C5QdYFRGdyqcThmC2/zdi.SmRSoDSkGtGB/5yfEbFeEsJ1qQ5El5e", 0, null });
        }
    }
}
