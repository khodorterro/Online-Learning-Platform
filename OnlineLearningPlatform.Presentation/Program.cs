using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.BusinessLayer.Services;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Repositories;
using OnlineLearningPlatform.Presentation.Mapping;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
