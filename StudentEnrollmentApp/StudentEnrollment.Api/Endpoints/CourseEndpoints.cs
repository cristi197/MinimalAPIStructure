using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Api.DTOs.Course;
using AutoMapper;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Course", async (ICourseRepository repo, IMapper mapper) =>
        {
            var courses = await repo.GetAllAsync();
            var data = mapper.Map<List<CourseDto>>(courses);
            return data;
        })
        .WithTags(nameof(Course))
        .WithName("GetAllCourses")
        .WithOpenApi();

        routes.MapGet("/api/Course/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("GetCourseById")
        .WithOpenApi();

        routes.MapGet("/api/Course/GetStudents/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentList(id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("GetCourseDetailsById")
        .WithOpenApi();

        routes.MapPut("/api/Course/{id}", async (int id, CourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(courseDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithTags(nameof(Course))
        .WithName("UpdateCourse")
        .WithOpenApi();

        routes.MapPost("/api/Course", async (CreateCourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            var course = mapper.Map<Course>(courseDto);
            await repo.AddAsync(course);
            return Results.Created($"/api/Course/{course.Id}", course);
        })
        .WithTags(nameof(Course))
        .WithName("CreateCourse")
        .WithOpenApi();

        routes.MapDelete("/api/Course/{id}", async (int id, ICourseRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(nameof(Course))
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
