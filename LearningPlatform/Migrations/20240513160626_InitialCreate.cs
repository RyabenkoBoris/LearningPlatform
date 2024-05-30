using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "SheduleSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonTime1 = table.Column<string>(type: "text", nullable: false),
                    LessonTime2 = table.Column<string>(type: "text", nullable: false),
                    LessonTime3 = table.Column<string>(type: "text", nullable: false),
                    LessonTime4 = table.Column<string>(type: "text", nullable: false),
                    LessonTime5 = table.Column<string>(type: "text", nullable: false),
                    LessonTime6 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departaments",
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
                    table.PrimaryKey("PK_Departaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departaments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    AcademicYear = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstSemester = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonNumber = table.Column<short>(type: "smallint", nullable: false),
                    isFirstWeek = table.Column<bool>(type: "boolean", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    SheduleSampleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_SheduleSamples_SheduleSampleId",
                        column: x => x.SheduleSampleId,
                        principalTable: "SheduleSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEntityTeacherEntity",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEntityTeacherEntity", x => new { x.CoursesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CourseEntityTeacherEntity_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEntityTeacherEntity_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonEntityTeacherEntity",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonEntityTeacherEntity", x => new { x.LessonsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_LessonEntityTeacherEntity_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonEntityTeacherEntity_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepartamentId = table.Column<int>(type: "integer", nullable: false),
                    SpecialityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Departaments_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilePaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilePaths_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassEntityGroupEntity",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "integer", nullable: false),
                    GroupsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassEntityGroupEntity", x => new { x.ClassesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_ClassEntityGroupEntity_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassEntityGroupEntity_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEntityGroupEntity",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    GroupsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEntityGroupEntity", x => new { x.CoursesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_CourseEntityGroupEntity_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEntityGroupEntity_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DepartamentId = table.Column<int>(type: "integer", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: true),
                    NumberId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departaments_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departaments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Numbers_NumberId",
                        column: x => x.NumberId,
                        principalTable: "Numbers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    JournalId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_AssessmentTypes_AssessmentTypeId",
                        column: x => x.AssessmentTypeId,
                        principalTable: "AssessmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntityUserEntity",
                columns: table => new
                {
                    JournalsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntityUserEntity", x => new { x.JournalsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_JournalEntityUserEntity_Journals_JournalsId",
                        column: x => x.JournalsId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssessmentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Лекція" },
                    { 2, "Практичне заняття" },
                    { 3, "Лабораторне заняття" },
                    { 4, "Модульна контрольна робота" },
                    { 5, "Розрахунково-графічна робота" },
                    { 6, "Домашня контрольна робота" },
                    { 7, "Залік" }
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
                table: "SheduleSamples",
                columns: new[] { "Id", "LessonTime1", "LessonTime2", "LessonTime3", "LessonTime4", "LessonTime5", "LessonTime6" },
                values: new object[] { 1, "8:30", "10:25", "12:20", "14:15", "16:10", "18:30" });

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 23, "Образотворче мистецтво, декоративне мистецтво, реставрація" },
                    { 2, 35, "Філологія" },
                    { 3, 51, "Економіка" },
                    { 4, 53, "Психологія" },
                    { 5, 54, "Соціологія" },
                    { 6, 61, "Журналістика" },
                    { 7, 72, "Фінанси, банківська справа та страхування" },
                    { 8, 73, "Менеджмент" },
                    { 9, 75, "Маркетинг" },
                    { 10, 81, "Право" },
                    { 11, 101, "Екологія" },
                    { 12, 104, "Фізика та астрономія" },
                    { 13, 105, "Прикладна фізика та наноматеріали" },
                    { 14, 111, "Математика" },
                    { 15, 113, "Прикладна математика" },
                    { 16, 121, "Інженерія програмного забезпечення" },
                    { 17, 122, "Комп’ютерні науки та інформаційні технології" },
                    { 18, 123, "Комп’ютерна інженерія" },
                    { 19, 124, "Системний аналіз" },
                    { 20, 125, "Кібербезпека" },
                    { 21, 126, "Інформаційні системи та технології" },
                    { 22, 131, "Прикладна механіка" },
                    { 23, 132, "Матеріалознавство" },
                    { 24, 133, "Галузеве машинобудування" },
                    { 25, 134, "Авіаційна та ракетно-космічна техніка" },
                    { 26, 136, "Металургія" },
                    { 27, 141, "Електроенергетика, електротехніка та електромеханіка" },
                    { 28, 142, "Енергетичне машинобудування" },
                    { 29, 143, "Атомна енергетика" },
                    { 30, 144, "Теплоенергетика" },
                    { 31, 161, "Хімічні технології та інженерія" },
                    { 32, 162, "Біотехнології та біоінженерія" },
                    { 33, 163, "Біомедична інженерія" },
                    { 34, 171, "Електроніка" },
                    { 35, 172, "Телекомунікації та радіотехніка" },
                    { 36, 173, "Авіоніка" },
                    { 37, 174, "Автоматизація, комп’ютерно-інтегровані технології та робототехніка" },
                    { 38, 175, "Інформаційно-вимірювальні технології" },
                    { 39, 176, "Мікро- та наносистемна техніка" },
                    { 40, 183, "Технології захисту навколишнього середовища" },
                    { 41, 184, "Гірництво" },
                    { 42, 186, "Видавництво та поліграфія" },
                    { 43, 227, "Фізична реабілітація" },
                    { 44, 231, "Соціальна робота" },
                    { 45, 281, "Публічне управління та адміністрування" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartamentId", "Email", "FirstName", "GroupId", "LastName", "MiddleName", "NumberId", "PasswordHash", "Role", "TeacherId" },
                values: new object[] { new Guid("f29cd9da-7d8a-4108-90dc-356f47bbf611"), null, "admin@admin", "Admin", null, "", "", null, "$2a$11$cUff033QxIj8OkkCCY.P4.QeFxp1iKaQobCJf0JNcMJw5qVUwDxvK", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_ClassEntityGroupEntity_GroupsId",
                table: "ClassEntityGroupEntity",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LessonId",
                table: "Classes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SheduleSampleId",
                table: "Classes",
                column: "SheduleSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntityGroupEntity_GroupsId",
                table: "CourseEntityGroupEntity",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntityTeacherEntity_TeachersId",
                table: "CourseEntityTeacherEntity",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_FacultyId",
                table: "Departaments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePaths_TaskId",
                table: "FilePaths",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AssessmentTypeId",
                table: "Grades",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_JournalId",
                table: "Grades",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherId",
                table: "Grades",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DepartamentId",
                table: "Groups",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialityId",
                table: "Groups",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntityUserEntity_UsersId",
                table: "JournalEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_LessonId",
                table: "Journals",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonEntityTeacherEntity_TeachersId",
                table: "LessonEntityTeacherEntity",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SectionId",
                table: "Tasks",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartamentId",
                table: "Users",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NumberId",
                table: "Users",
                column: "NumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherId",
                table: "Users",
                column: "TeacherId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassEntityGroupEntity");

            migrationBuilder.DropTable(
                name: "CourseEntityGroupEntity");

            migrationBuilder.DropTable(
                name: "CourseEntityTeacherEntity");

            migrationBuilder.DropTable(
                name: "FilePaths");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "JournalEntityUserEntity");

            migrationBuilder.DropTable(
                name: "LessonEntityTeacherEntity");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "AssessmentTypes");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SheduleSamples");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Numbers");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
