using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

//[AuthorizeAsAdmin]
public class EateriesController : BaseController
{
    private readonly IRepository<Eatery> _eateryRepository;

    public EateriesController(IRepository<Eatery> eateryRepository)
    {
        _eateryRepository = eateryRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<EateryDto>>> GetAll()
    {
        var eateries = await _eateryRepository.GetAllAsync();
        
        var eateriesDtos = eateries.Select(eatery => new EateryDto
        {
            Name = eatery.Name,
            Address = eatery.Address,
            Type = eatery.Type,
            OpeningDay = eatery.OpeningDay
        }).ToList();
        
        return eateriesDtos;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<EateryDto>> GetById(Guid id)
    {
        var result = await _eateryRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Eatery is not found");
        
        return new EateryDto
        {
            Name = result.Name,
            Address = result.Address,
            Type = result.Type,
            OpeningDay = result.OpeningDay
        };
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EateryDto dto)
    {
        var eatery = new Eatery()
        {
            Name = dto.Name,
            Address = dto.Address,
            Type = dto.Type,
            OpeningDay = dto.OpeningDay
        };
        
        await _eateryRepository.CreateAsync(eatery);
        
        return CreatedAtAction(nameof(GetById), new { id = eatery.Id }, eatery);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] EateryDto dto)
    {
        var existingEatery = await _eateryRepository.GetByIdAsync(id);

        if (existingEatery == null)
            return BadRequest("Eatery not found");

        existingEatery.Name = dto.Name;
        existingEatery.Address = dto.Address;
        existingEatery.Type = dto.Type;
        existingEatery.OpeningDay = dto.OpeningDay;

        await _eateryRepository.UpdateAsync(existingEatery);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _eateryRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}