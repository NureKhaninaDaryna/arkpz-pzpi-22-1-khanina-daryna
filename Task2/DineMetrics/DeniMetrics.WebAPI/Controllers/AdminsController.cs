using DeniMetrics.WebAPI.Attributes;
using DineMetrics.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

[AuthorizeAsAdmin]
public class AdminsController : BaseController
{
    private readonly IEateryService _eateryService;

    public AdminsController(IEateryService eateryService)
    {
        _eateryService = eateryService;
    }

    [HttpPut("{id}/operating-hours")]
    public async Task<IActionResult> UpdateOperatingHours(Guid id, [FromBody] UpdateOperatingHoursDto dto)
    {
        var result = await _eateryService.UpdateOperatingHours(id, dto.From, dto.To);

        if (!result.IsSuccess)
            return BadRequest(result.Error.Message);

        return Ok(result.Value);
    }

    [HttpPut("{id}/maximum-capacity")]
    public async Task<IActionResult> UpdateMaximumCapacity(Guid id, [FromBody] UpdateMaximumCapacityDto dto)
    {
        var result = await _eateryService.UpdateMaximumCapacity(id, dto.Capacity);

        if (!result.IsSuccess)
            return BadRequest(result.Error.Message);

        return Ok(result.Value);
    }

    [HttpPut("{id}/temperature-threshold")]
    public async Task<IActionResult> UpdateTemperatureThreshold(Guid id, [FromBody] UpdateTemperatureThresholdDto dto)
    {
        var result = await _eateryService.UpdateTemperatureThreshold(id, dto.MinTemperature);

        if (!result.IsSuccess)
            return BadRequest(result.Error.Message);

        return Ok(result.Value);
    }
}

public class UpdateOperatingHoursDto
{
    public string From { get; set; }
    public string To { get; set; }
}

public class UpdateMaximumCapacityDto
{
    public int Capacity { get; set; }
}

public class UpdateTemperatureThresholdDto
{
    public double MinTemperature { get; set; }
}