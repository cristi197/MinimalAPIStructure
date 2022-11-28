using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using AutoMapper;
using StudentEnrollment.Api.DTOs.Student;
using StudentEnrollment.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using StudentEnrollment.Api.DTOs.Enrollment;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Student", async (IStudentRepository repo, IMapper mapper) =>
        {
            var students = await repo.GetAllAsync();
            var data = mapper.Map<List<StudentDto>>(students);
            return data;
        })
        .AllowAnonymous()
        .WithTags(nameof(Student))
        .WithName("GetAllStudents")
        .WithOpenApi();

        routes.MapGet("/api/Student/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Student model
                    ? Results.Ok(mapper.Map<Student>(model))
                    : Results.NotFound();
        })
        .AllowAnonymous()
        .WithTags(nameof(Student))
        .WithName("GetStudentById")
        .WithOpenApi();

        routes.MapGet("/api/Student/GetDetails/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentDetails(id)
                is Student model
                    ? Results.Ok(mapper.Map<StudentDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Student))
        .WithName("GetStudentDetailsById")
        .WithOpenApi();

        routes.MapPut("/api/Student/{id}",[Authorize(Roles ="Administrator")] async (int id, StudentDto studentDto, IStudentRepository repo, IMapper mapper, IValidator<StudentDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var foundModel = await repo.GetAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(studentDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithTags(nameof(Student))
        .WithName("UpdateStudent")
        .WithOpenApi();

        routes.MapPost("/api/Student", async (CreateStudentDto studentDto, IStudentRepository repo, IMapper mapper, IValidator<CreateStudentDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var student = mapper.Map<Student>(studentDto);
            await repo.AddAsync(student);
            return Results.Created($"/api/Student/{student.Id}", student);
        })
        .WithTags(nameof(Student))
        .WithName("CreateStudent")
        .WithOpenApi();

        routes.MapDelete("/api/Student/{id}", [Authorize(Roles = "Administrator")] async (int id, IStudentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(nameof(Student))
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}
