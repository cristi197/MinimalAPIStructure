using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Endpoints;
using StudentEnrollment.Api.Configurations;
using StudentEnrollment.Data.Repositories;
using StudentEnrollment.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentEnrollment.Api.Services;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection");
builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddIdentityCore<SchoolUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StudentEnrollmentDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IFileUpload, FileUpload>();


builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapStudentEndpoints();

app.MapEnrollmentEndpoints();

app.MapCourseEndpoints();

app.MapAuthenticationEndpoints();

app.Run();
