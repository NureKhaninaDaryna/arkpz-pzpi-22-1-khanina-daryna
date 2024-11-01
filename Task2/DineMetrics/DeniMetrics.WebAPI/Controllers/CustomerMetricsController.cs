using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class CustomerMetricsController : BaseController
{
    private readonly IRepository<CustomerMetric> _customerMetricRepository;
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<Report> _reportRepository;

    public CustomerMetricsController(IRepository<CustomerMetric> customerMetricRepository, IRepository<Device> deviceRepository, IRepository<Report> reportRepository)
    {
        _customerMetricRepository = customerMetricRepository;
        _deviceRepository = deviceRepository;
        _reportRepository = reportRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerMetricDto>>> GetAll()
    {
        var customerMetrics = await _customerMetricRepository.GetAllAsync();
        
        var customerMetricDtos = customerMetrics.Select(metric => new CustomerMetricDto
        {
            Count = metric.Count,
            Time = metric.Time,
            DeviceId = metric.Device.Id
        }).ToList();

        return customerMetricDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerMetricDto>> GetById(Guid id)
    {
        var result = await _customerMetricRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("CustomerMetric not found");
        
        return new CustomerMetricDto()
        {
            Count = result.Count,
            Time = result.Time,
            DeviceId = result.Device.Id
        };
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CustomerMetricDto dto)
    {
        var device = await _deviceRepository.GetByIdAsync(dto.DeviceId);

        if (device is null)
            return BadRequest("Device not found");
        
        var metric = new CustomerMetric
        {
            Count = dto.Count,
            Time = dto.Time,
            Device = device
        };
        
        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        var todayReport = await _reportRepository.GetByPredicateAsync(r => r.ReportDate == currentDate);

        if (todayReport.Count > 0)
        {
            metric.Report = todayReport.FirstOrDefault()!;

            await UpdateReport(metric);
        }
        else
        {
            var report = new Report()
            {
                ReportDate = currentDate
            };

            await _reportRepository.CreateAsync(report);
            
            metric.Report = report;
        }

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

    private async Task UpdateReport(CustomerMetric metric)
    {
        metric.Report.TotalCustomers += metric.Count;

        await _reportRepository.UpdateAsync(metric.Report);
    }
}
