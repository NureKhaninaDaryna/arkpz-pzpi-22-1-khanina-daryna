using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class TemperatureMetricsController : BaseController
{
    private readonly IRepository<TemperatureMetric> _temperatureMetricRepository;

    public TemperatureMetricsController(IRepository<TemperatureMetric> temperatureMetricRepository)
    {
        _temperatureMetricRepository = temperatureMetricRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TemperatureMetric>>> GetAll()
    {
        return await _temperatureMetricRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemperatureMetric>> GetById(Guid id)
    {
        var result = await _temperatureMetricRepository.GetByIdAsync(id);
        
        if (result is null)
            return BadRequest("TemperatureMetric not found");
        
        return result;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TemperatureMetricDto dto)
    {
        var metric = new TemperatureMetric
        {
            // Map properties from dto to model
        };

        await _temperatureMetricRepository.CreateAsync(metric);

        return CreatedAtAction(nameof(GetById), new { id = metric.Id }, metric);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] TemperatureMetric metric)
    {
        if (id != metric.Id)
            return BadRequest("TemperatureMetric ID mismatch");

        await _temperatureMetricRepository.UpdateAsync(metric);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _temperatureMetricRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}