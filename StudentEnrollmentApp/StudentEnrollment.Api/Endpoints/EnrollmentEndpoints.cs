﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment));

        group.MapGet("/", async (StudentEnrollmentDbContext db) =>
        {
            return await db.Enrollments.ToListAsync();
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Enrollment>, NotFound>> (int id, StudentEnrollmentDbContext db) =>
        {
            return await db.Enrollments.FindAsync(id)
                is Enrollment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEnrollmentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Enrollment enrollment, StudentEnrollmentDbContext db) =>
        {
            var foundModel = await db.Enrollments.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            db.Update(enrollment);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateEnrollment")
        .WithOpenApi();

        group.MapPost("/", async (Enrollment enrollment, StudentEnrollmentDbContext db) =>
        {
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Enrollment/{enrollment.Id}", enrollment);
        })
        .WithName("CreateEnrollment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Enrollment>, NotFound>> (int id, StudentEnrollmentDbContext db) =>
        {
            if (await db.Enrollments.FindAsync(id) is Enrollment enrollment)
            {
                db.Enrollments.Remove(enrollment);
                await db.SaveChangesAsync();
                return TypedResults.Ok(enrollment);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteEnrollment")
        .WithOpenApi();
    }
}
