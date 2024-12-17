using DineMetrics.Core.Dto;
using FluentValidation;

namespace DeniMetrics.WebAPI.Validators;

public class TemperatureMetricDtoValidator : AbstractValidator<TemperatureMetricDto>
{
    public TemperatureMetricDtoValidator()
    {
        RuleFor(x => x.DeviceId)
            .NotEmpty()
            .WithMessage("DeviceId is required.")
            .Must(id => id != Guid.Empty)
            .WithMessage("DeviceId must be a valid, non-empty GUID.");

        RuleFor(x => x.Value)
            .InclusiveBetween(-100, 100)
            .WithMessage("Temperature value must be between -100 and 100.");

        RuleFor(x => x.Time)
            .NotEmpty()
            .WithMessage("Time is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Time cannot be in the future.");
    }
}