using AutoMapper;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Api.DTOs;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (LoginDto loginDto, IAuthManager authManager) =>
            {
                //Generate token here....
                var response = await authManager.Login(loginDto);

                if(response is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);

            })
            .WithTags("Authentication")
            .WithName("Login")
            .WithOpenApi();

            routes.MapPost("/api/register/", async (RegisterDto registerDto, IAuthManager authManager) =>
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
            .WithTags("Authentication")
            .WithName("Register")
            .WithOpenApi();
        }
    }
}
