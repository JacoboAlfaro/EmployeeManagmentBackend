using EmployeeApi.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validators
{
    public class CreateDepartmentRequestValidator: AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentRequestValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("El nombre del departamento es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del departamento no puede exceder los 100 caracteres.");
        }
    }
    public class UpdateDepartmentRequestValidator : AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentRequestValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("El nombre del departamento es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del departamento no puede exceder los 100 caracteres.");
        }
    }
}
