using DineMetrics.Core.Dto;
using FluentValidation;

namespace DeniMetrics.WebAPI.Validators;

public class CustomerMetricDtoValidator : AbstractValidator<CustomerMetricDto>
{
    public CustomerMetricDtoValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage("Count must be a positive integer.");

        RuleFor(x => x.Time)
            .NotEmpty()
            .WithMessage("Time is required.");

        RuleFor(x => x.DeviceId)
            .NotEmpty()
            .WithMessage("DeviceId is required.")
            .Must(id => id != Guid.Empty)
            .WithMessage("DeviceId must be a valid, non-empty GUID.");
    }
}