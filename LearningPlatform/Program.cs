using LearningPlatform;
using LearningPlatform.Authentication;
using LearningPlatform.Authorization;
using LearningPlatform.Interfaces;
using LearningPlatform.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISpecialitiesRepository, SpecialitiesRepository>();
builder.Services.AddScoped<ILessonsRepository, LessonsRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IAdminUsersRepository, AdminUsersRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();
builder.Services.AddScoped<IClassesRepository, ClassesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<ISectionsRepository, SectionsRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IFilesRepository, FilesRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IStudentTasksRepository, StudentTasksRepository>();
builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<IGradesRepository, GradesRepository>();
builder.Services.AddScoped<IAssesmentTypesRepository, AssesmentTypesRepository>();
//AuthorizationHandlers
builder.Services.AddTransient<IAuthorizationHandler, CourseAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, CourseManipulationAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, MemberCourseAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, SectionManipulationAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, TaskManipulationAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, TaskAccessAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, FileDownloadAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, FileDeleteAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, StudentTaskManipulationAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, StudentWriteCommentAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, DeleteCommentAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, StudentTaskTeacherAccessAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, TeacherWriteCommentAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, JournalTeacherAccessAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, JournalStudentAccessAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, RateAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, GradeAuthorizationHandler>();


builder.Services.AddDbContext<LearningDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
    });


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MemberCourse", policy => policy.Requirements.Add(new MemberCourseRequirement()));
    options.AddPolicy("TeacherCourse", policy => policy.Requirements.Add(new OwnerTeachersRequirement()));
    options.AddPolicy("CourseManipulation", policy => policy.Requirements.Add(new TeacherRequirement()));
    options.AddPolicy("SectionManipulation", policy => policy.Requirements.Add(new SectionManipularionRequirement()));
    options.AddPolicy("TaskManipulation", policy => policy.Requirements.Add(new TaskManipulationRequirement()));
    options.AddPolicy("TaskAccess", policy => policy.Requirements.Add(new TaskAccessRequirement()));
    options.AddPolicy("FileDownload", policy => policy.Requirements.Add(new FileDownloadRequirement()));
    options.AddPolicy("FileDelete", policy => policy.Requirements.Add(new FileDeleteRequirement()));
    options.AddPolicy("StudentTaskManipulation", policy => policy.Requirements.Add(new StudentTaskManipulationRequirement()));
    options.AddPolicy("StudentWriteComment", policy => policy.Requirements.Add(new StudentWriteCommentRequirement()));
    options.AddPolicy("DeleteComment", policy => policy.Requirements.Add(new DeleteCommentRequirement()));
    options.AddPolicy("StudentTaskTeacherAccess", policy => policy.Requirements.Add(new StudentTaskTeacherAccessRequirement()));
    options.AddPolicy("TeacherWriteComment", policy => policy.Requirements.Add(new TeacherWriteCommentRequirement()));
    options.AddPolicy("JournalTeacherAccess", policy => policy.Requirements.Add(new JournalTeacherAccessRequirement()));
    options.AddPolicy("JournalStudentAccess", policy => policy.Requirements.Add(new JournalStudentAccessRequirement()));
    options.AddPolicy("Rate", policy => policy.Requirements.Add(new RateRequirement()));
    options.AddPolicy("Grade", policy => policy.Requirements.Add(new GradeRequirement()));
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shedule}/{action=Index}/{id?}");

app.Run();
