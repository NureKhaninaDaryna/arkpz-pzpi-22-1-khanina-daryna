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
}
