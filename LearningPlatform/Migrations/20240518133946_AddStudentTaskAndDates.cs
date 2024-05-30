using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTaskAndDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePaths_Tasks_TaskId",
                table: "FilePaths");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f82788a6-8c56-45d6-8220-c387938e0879"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StudentTaskId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "FilePaths",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "StudentTaskId",
                table: "FilePaths",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("1f78d5af-b77b-4535-a55c-9c019f428b38"), null, "admin@admin", null, "Admin", "$2a$11$gmNsIAGQ4L12dRRhozgHIOPw4F0SOs7seSModOtysMfObkvuPcDMi", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_FilePaths_StudentTaskId",
                table: "FilePaths",
                column: "StudentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTasks_TaskId",
                table: "StudentTasks",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTasks_UserId",
                table: "StudentTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePaths_StudentTasks_StudentTaskId",
                table: "FilePaths",
                column: "StudentTaskId",
                principalTable: "StudentTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePaths_Tasks_TaskId",
                table: "FilePaths",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePaths_StudentTasks_StudentTaskId",
                table: "FilePaths");

            migrationBuilder.DropForeignKey(
                name: "FK_FilePaths_Tasks_TaskId",
                table: "FilePaths");

            migrationBuilder.DropTable(
                name: "StudentTasks");

            migrationBuilder.DropIndex(
                name: "IX_FilePaths_StudentTaskId",
                table: "FilePaths");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f78d5af-b77b-4535-a55c-9c019f428b38"));

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StudentTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StudentTaskId",
                table: "FilePaths");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "FilePaths",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("f82788a6-8c56-45d6-8220-c387938e0879"), null, "admin@admin", null, "Admin", "$2a$11$FtoLqAnzEGW63rw1H2eFpu1AiLvmBmD3UYDENXl5cjxPiX4ogngIm", 0, null });

            migrationBuilder.AddForeignKey(
                name: "FK_FilePaths_Tasks_TaskId",
                table: "FilePaths",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
