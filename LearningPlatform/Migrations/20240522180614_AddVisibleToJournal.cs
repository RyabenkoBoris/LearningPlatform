using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddVisibleToJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b957f332-7b2b-49aa-8eb3-dc548c4c7ea9"));

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Journals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("87438678-fb1e-4118-b378-bd2c20ac0f8b"), null, "admin@admin", null, "Admin", "$2a$11$C5QdYFRGdyqcThmC2/zdi.SmRSoDSkGtGB/5yfEbFeEsJ1qQ5El5e", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87438678-fb1e-4118-b378-bd2c20ac0f8b"));

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Journals");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("b957f332-7b2b-49aa-8eb3-dc548c4c7ea9"), null, "admin@admin", null, "Admin", "$2a$11$D8YmEBembDEchpO2JsxRR.djLo50btNK5ubk6jenHa6PzXSMaToqm", 0, null });
        }
    }
}
