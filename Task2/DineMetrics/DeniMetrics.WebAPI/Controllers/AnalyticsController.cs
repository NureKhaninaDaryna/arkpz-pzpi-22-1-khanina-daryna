using DineMetrics.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class AnalyticsController : BaseController
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    
    [HttpGet("dashboard-metrics")]
    public async Task<IActionResult> GetDashboardMetrics([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var result = await _analyticsService.GetDashboardMetrics(from, to);

        if (!result.IsSuccess)
        {
            if (result.Error?.Message == "Not found")
                return NotFound(new { Message = "No metrics found for the specified period." });

            return BadRequest(new { Message = result.Error?.Message });
        }

        return Ok(result.Value);
    }
    
    [HttpGet("trends/{facilityId:guid}")]
    public async Task<IActionResult> GenerateTrends(Guid facilityId)
    {
        var result = await _analyticsService.GenerateTrends(facilityId);

        if (!result.IsSuccess)
        {
            if (result.Error?.Message == "Not found")
                return NotFound(new { Message = "No metrics found for the specified facility." });

            return BadRequest(new { Message = result.Error?.Message });
        }

        return Ok(result.Value);
    }
}