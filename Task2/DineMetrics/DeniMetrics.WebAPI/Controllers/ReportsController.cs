using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class ReportsController : BaseController
{
    private readonly IRepository<Report> _reportRepository;

    public ReportsController(IRepository<Report> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportDto>>> GetAll()
    {
        var reports = await _reportRepository.GetAllAsync();
        
        var reportDtos = reports.Select(report => new ReportDto
        {
            ReportDate = report.ReportDate,
            TotalCustomers = report.TotalCustomers,
            AverageTemperature = report.AverageTemperature
        }).ToList();
        
        return reportDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReportDto>> GetById(Guid id)
    {
        var result = await _reportRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Report not found");
        
        return new ReportDto
        {
            ReportDate = result.ReportDate,
            TotalCustomers = result.TotalCustomers,
            AverageTemperature = result.AverageTemperature
        };
    }
}
