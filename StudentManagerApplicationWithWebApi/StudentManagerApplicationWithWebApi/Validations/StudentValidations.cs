using FluentValidation;
using FluentValidation.AspNetCore;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.DTO;

namespace StudentManagerApplicationWithWebApi.Validations
{
    public class StudentValidations:AbstractValidator<StudentDto>
    {
        public StudentValidations()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("It is not an email adress");
            RuleFor(x => x.Name).NotEmpty().WithMessage("It can not be empty!!");
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Phone).LessThan(5);
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage("It can't be null");
        }

    
    }
}
