using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using AutoMapper;
using StudentEnrollment.Api.DTOs.Student;

namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Student", async (StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var students = await db.Students.ToListAsync();
            var data = mapper.Map<List<StudentDto>>(students);
            return data;
        })
        .WithName("GetAllStudents")
        .WithOpenApi();

        routes.MapGet("/api/Student/{id}", async (int id, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            return await db.Students.FindAsync(id)
                is Student model
                    ? Results.Ok(mapper.Map<Student>(model))
                    : Results.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        routes.MapPut("/api/Student/{id}", async (int id, StudentDto studentDto, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.Students.FindAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(studentDto, foundModel);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        routes.MapPost("/api/Student", async (CreateStudentDto studentDto, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var student = mapper.Map<Student>(studentDto);
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Student/{student.Id}", student);
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        routes.MapDelete("/api/Student/{id}", async Task<Results<Ok<Student>, NotFound>> (int id, StudentEnrollmentDbContext db) =>
        {
            if (await db.Students.FindAsync(id) is Student student)
            {
                db.Students.Remove(student);
                await db.SaveChangesAsync();
                return TypedResults.Ok(student);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}
