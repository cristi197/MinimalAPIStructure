using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(x=> x.Id).NotEmpty();
            RuleFor(x=> x.Title).NotEmpty();
            RuleFor(x=> x.Credits).NotEmpty().GreaterThan(0);
        }
    }
}
