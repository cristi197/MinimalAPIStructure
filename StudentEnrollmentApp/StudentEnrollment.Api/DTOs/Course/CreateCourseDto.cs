﻿using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Course
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public int Credits { get; set; }
    }
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(x=> x.Title).NotEmpty();
            RuleFor(x=> x.Credits).NotEmpty().GreaterThan(0);
        }
    }

}
