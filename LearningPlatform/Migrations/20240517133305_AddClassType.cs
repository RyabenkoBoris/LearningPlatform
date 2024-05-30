using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ab3eface-dfc5-44ba-bffe-1579869b0134"));

            migrationBuilder.AddColumn<int>(
                name: "ClassTypeId",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClassTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Лек on-line" },
                    { 2, "Прак on-line" },
                    { 3, "Лаб on-line" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("69178df2-f757-45cc-80aa-be82f3da1c61"), null, "admin@admin", null, "Admin", "$2a$11$Q34M4TiwP/N4O/zelsYviOM0INIgvBqGB2/3BB7nzfIf3.0M9jHGK", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTypeId",
                table: "Classes",
                column: "ClassTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ClassTypes_ClassTypeId",
                table: "Classes",
                column: "ClassTypeId",
                principalTable: "ClassTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ClassTypes_ClassTypeId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassTypeId",
                table: "Classes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69178df2-f757-45cc-80aa-be82f3da1c61"));

            migrationBuilder.DropColumn(
                name: "ClassTypeId",
                table: "Classes");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("ab3eface-dfc5-44ba-bffe-1579869b0134"), null, "admin@admin", null, "Admin", "$2a$11$23EzkGVgfrauLpSCTuSPsO2WTrG32DbzZ3qSVQeLf9fM8C2XwO6ri", 0, null });
        }
    }
}
