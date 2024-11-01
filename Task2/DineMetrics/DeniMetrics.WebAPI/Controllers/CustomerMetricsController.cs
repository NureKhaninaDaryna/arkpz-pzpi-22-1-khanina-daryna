using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class CustomerMetricsController : BaseController
{
    private readonly IRepository<CustomerMetric> _customerMetricRepository;

    public CustomerMetricsController(IRepository<CustomerMetric> customerMetricRepository)
    {
        _customerMetricRepository = customerMetricRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerMetric>>> GetAll()
    {
        return await _customerMetricRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerMetric>> GetById(Guid id)
    {
        var result = await _customerMetricRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("CustomerMetric not found");
        
        return result;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CustomerMetricDto dto)
    {
        var metric = new CustomerMetric
        {
            // Map properties from dto to model
        };

        await _customerMetricRepository.CreateAsync(metric);

        return CreatedAtAction(nameof(GetById), new { id = metric.Id }, metric);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] CustomerMetric metric)
    {
        if (id != metric.Id)
            return BadRequest("CustomerMetric ID mismatch");

        await _customerMetricRepository.UpdateAsync(metric);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _customerMetricRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}
