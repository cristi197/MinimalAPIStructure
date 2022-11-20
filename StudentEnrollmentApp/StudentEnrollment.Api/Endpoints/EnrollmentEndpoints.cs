using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using AutoMapper;
using System.Net;
using StudentEnrollment.Api.DTOs.Enrollment;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Enrollment", async (IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollments = await repo.GetAllAsync();
            var data = mapper.Map<List<EnrollmentDto>>(enrollments);
            return data;
        })
        .WithTags(nameof(Enrollment))
        .WithName("GetAllEnrollments")
        .WithOpenApi();

        routes.MapGet("/api/Enrollment/{id}", async (int id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Enrollment model
                    ? Results.Ok(mapper.Map<Enrollment>(model))
                    : Results.NotFound();
        })
        .WithTags(nameof(Enrollment))
        .WithName("GetEnrollmentById")
        .WithOpenApi();

        routes.MapPut("/api/Enrollment/{id}", async (int id, EnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(enrollmentDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithTags(nameof(Enrollment))
        .WithName("UpdateEnrollment")
        .WithOpenApi();

        routes.MapPost("/api/Enrollment", async (CreateEnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollment = mapper.Map<Enrollment>(enrollmentDto);
            await repo.AddAsync(enrollment);
            return Results.Created($"/api/Enrollment/{enrollment.Id}", enrollment);
        })
        .WithTags(nameof(Enrollment))
        .WithName("CreateEnrollment")
        .WithOpenApi();

        routes.MapDelete("/api/Enrollment/{id}", async (int id, IEnrollmentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithTags(nameof(Enrollment))
        .WithName("DeleteEnrollment")
        .WithOpenApi();
    }
}
