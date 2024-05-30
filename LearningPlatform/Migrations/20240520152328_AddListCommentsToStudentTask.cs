using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddListCommentsToStudentTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("804101dd-6316-4021-a25b-3cb681ba290c"));

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "StudentTasks");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    StudentTaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_StudentTasks_StudentTaskId",
                        column: x => x.StudentTaskId,
                        principalTable: "StudentTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("2ae05e92-0502-4d08-a557-3f9325c3c9d4"), null, "admin@admin", null, "Admin", "$2a$11$EnuVpgDMj/k9vtriZVZjZ.ECSe4Wk42pr6/jT5GPNzKsKzjTZtm4G", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentTaskId",
                table: "Comments",
                column: "StudentTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ae05e92-0502-4d08-a557-3f9325c3c9d4"));

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "StudentTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("804101dd-6316-4021-a25b-3cb681ba290c"), null, "admin@admin", null, "Admin", "$2a$11$a4hwAK1eYY1qh4Tn9tJU9.rJzAsWXIVeY9/oYcbyB2GMhwcUSxO8m", 0, null });
        }
    }
}
