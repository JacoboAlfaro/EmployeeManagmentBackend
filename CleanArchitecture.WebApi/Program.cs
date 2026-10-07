using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Interfaces.IRepositories;
using CleanArchitecture.Application.Interfaces.IServices;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Validators;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.WebApi.Filters;
using CleanArchitecture.WebApi.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddDbContext<EmployeeDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Activa todos las validaciones de FluentValidation en el ensamblado que contiene CreateEmployeeRequestValidator
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IFileStorageService>(sp => {
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    var imagesPath = Path.Combine(
        env.WebRootPath,
        "images"
    );

    return new FileStorageService(imagesPath);
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("mypolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("mypolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
