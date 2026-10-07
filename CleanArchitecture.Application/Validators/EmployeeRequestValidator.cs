using EmployeeApi.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Validators
{
    public class CreateEmployeeRequestValidator: AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.fullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre completo no puede exceder los 150 caracteres.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");

            RuleFor(x => x.salary)
                .GreaterThan(0).WithMessage("El salario debe ser mayor que 0.");

            RuleFor(x => x.departmentId)
                .GreaterThan(0).WithMessage("El departamento es obligatorio.");
        }
    }

    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.fullName)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre completo no puede exceder los 150 caracteres.");
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");
            RuleFor(x => x.salary)
                .GreaterThan(0).WithMessage("El salario debe ser mayor que 0.");
            RuleFor(x => x.departmentId)
                .GreaterThan(0).WithMessage("El departamento es obligatorio.");
        }
    }
}
