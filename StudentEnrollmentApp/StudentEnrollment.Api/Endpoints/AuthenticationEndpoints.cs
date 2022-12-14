using AutoMapper;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Api.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using StudentEnrollment.Api.Filters;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (LoginDto loginDto, IAuthManager authManager, IValidator<LoginDto> validator) =>
            {
                //Generate token here....
                var response = await authManager.Login(loginDto);

                if(response is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);

            })
            .AddEndpointFilter<ValidationFilter<LoginDto>>()
            .AllowAnonymous()
            .WithTags("Authentication")
            .WithName("Login")
            .WithOpenApi();

            routes.MapPost("/api/register/", async (RegisterDto registerDto, IAuthManager authManager, IValidator<RegisterDto> validator) =>
            {
                var response = await authManager.Register(registerDto);

                if (!response.Any())
                {
                    return Results.Ok();
                }

                var errors = new List<ErrorResponseDto>();

                foreach(var error in response)
                {
                    errors.Add(new ErrorResponseDto
                    {
                        Code = error.Code,
                        Description = error.Description,
                    });
                }

                return Results.BadRequest(errors);
            })
            .AddEndpointFilter<ValidationFilter<RegisterDto>>()
            .AllowAnonymous()
            .WithTags("Authentication")
            .WithName("Register")
            .WithOpenApi();
        }
    }
}
