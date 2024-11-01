using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class TemperatureMetricsController : BaseController
{
    private readonly IRepository<TemperatureMetric> _temperatureMetricRepository;
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<Report> _reportRepository;

    public TemperatureMetricsController(IRepository<TemperatureMetric> temperatureMetricRepository, IRepository<Device> deviceRepository, IRepository<Report> reportRepository)
    {
        _temperatureMetricRepository = temperatureMetricRepository;
        _deviceRepository = deviceRepository;
        _reportRepository = reportRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TemperatureMetricDto>>> GetAll()
    {
        var temperatureMetrics = await _temperatureMetricRepository.GetAllAsync();
        
        var temperatureMetricDtos = temperatureMetrics.Select(metric => new TemperatureMetricDto
        {
            DeviceId = metric.Device.Id,
            Value = metric.Value,
            Time = metric.Time,
        }).ToList();
        
        return temperatureMetricDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemperatureMetricDto>> GetById(Guid id)
    {
        var result = await _temperatureMetricRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("TemperatureMetric not found");
        
        return new TemperatureMetricDto
        {
            DeviceId = result.Device.Id,
            Value = result.Value,
            Time = result.Time,
        };
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TemperatureMetricDto dto)
    {
        var device = await _deviceRepository.GetByIdAsync(dto.DeviceId);

        if (device is null)
            return BadRequest("Device not found");
        
        var metric = new TemperatureMetric
        {
            Value = dto.Value,
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
    
    private async Task UpdateReport(TemperatureMetric metric)
    {
        metric.Report.AverageTemperature += metric.Value;

        await _reportRepository.UpdateAsync(metric.Report);
    }
}