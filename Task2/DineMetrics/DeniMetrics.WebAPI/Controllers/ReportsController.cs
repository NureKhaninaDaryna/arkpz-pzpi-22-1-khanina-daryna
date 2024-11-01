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
    public async Task<ActionResult<List<Report>>> GetAll()
    {
        return await _reportRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Report>> GetById(Guid id)
    {
        var result = await _reportRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Report not found");
        
        return result;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ReportDto dto)
    {
        var report = new Report
        {
            // Map properties from dto to model
        };

        await _reportRepository.CreateAsync(report);

        return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Report report)
    {
        if (id != report.Id)
            return BadRequest("Report ID mismatch");

        await _reportRepository.UpdateAsync(report);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _reportRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}
