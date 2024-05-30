using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Departaments_DepartamentId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departaments_DepartamentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Numbers_NumberId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.DropTable(
                name: "Numbers");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartamentId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a30fe03f-2a8c-43e9-aaa6-4ca8321241c7"));

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "NumberId",
                table: "Users",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_NumberId",
                table: "Users",
                newName: "IX_Users_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "Groups",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DepartamentId",
                table: "Groups",
                newName: "IX_Groups_DepartmentId");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: false),
                    FacultyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "Email", "GroupId", "Name", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("2ee1ce7c-06f8-483b-905d-7fe771310227"), null, "admin@admin", null, "Admin", "$2a$11$WLzvsBeUjBMC4NePA.9iSeDxQBdYP5tCDFs.rci8.37mrgvsc8kjS", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Departments_DepartmentId",
                table: "Groups",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Departments_DepartmentId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ee1ce7c-06f8-483b-905d-7fe771310227"));

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Users",
                newName: "NumberId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                newName: "IX_Users_NumberId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Groups",
                newName: "DepartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DepartmentId",
                table: "Groups",
                newName: "IX_Groups_DepartamentId");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacultyId = table.Column<int>(type: "integer", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departaments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numbers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Numbers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "01" },
                    { 2, "02" },
                    { 3, "03" },
                    { 4, "04" },
                    { 5, "11" },
                    { 6, "12" },
                    { 7, "13" },
                    { 8, "14" },
                    { 9, "21" },
                    { 10, "22" },
                    { 11, "23" },
                    { 12, "24" },
                    { 13, "31" },
                    { 14, "32" },
                    { 15, "33" },
                    { 16, "34" },
                    { 17, "41" },
                    { 18, "42" },
                    { 19, "43" },
                    { 20, "44" },
                    { 21, "51" },
                    { 22, "52" },
                    { 23, "53" },
                    { 24, "54" },
                    { 25, "61" },
                    { 26, "62" },
                    { 27, "63" },
                    { 28, "64" },
                    { 29, "71" },
                    { 30, "72" },
                    { 31, "73" },
                    { 32, "74" },
                    { 33, "81" },
                    { 34, "82" },
                    { 35, "83" },
                    { 36, "84" },
                    { 37, "91" },
                    { 38, "92" },
                    { 39, "93" },
                    { 40, "94" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartamentId", "Email", "GroupId", "Name", "NumberId", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("a30fe03f-2a8c-43e9-aaa6-4ca8321241c7"), null, "admin@admin", null, "Admin", null, "$2a$11$d6x.xeLkp6DjkEjPdQuXnew2m.fu3hyT.G6j60VjXRElrp56I/vXu", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartamentId",
                table: "Users",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_FacultyId",
                table: "Departaments",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Departaments_DepartamentId",
                table: "Groups",
                column: "DepartamentId",
                principalTable: "Departaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departaments_DepartamentId",
                table: "Users",
                column: "DepartamentId",
                principalTable: "Departaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Numbers_NumberId",
                table: "Users",
                column: "NumberId",
                principalTable: "Numbers",
                principalColumn: "Id");
        }
    }
}
