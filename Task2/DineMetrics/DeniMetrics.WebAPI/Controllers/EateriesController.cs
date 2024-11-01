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
    public async Task<ActionResult<List<Eatery>>> GetAll()
    {
        return await _eateryRepository.GetAllAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Eatery>> GetById(Guid id)
    {
        var result = await _eateryRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Eatery is not found");
        
        return result;
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
    public async Task<ActionResult> Update(Guid id, [FromBody] Eatery eatery)
    {
        if (id != eatery.Id)
            return BadRequest("Eatery ID mismatch");

        await _eateryRepository.UpdateAsync(eatery);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _eateryRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}