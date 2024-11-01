﻿using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class DevicesController : BaseController
{
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<Eatery> _eateryRepository;

    public DevicesController(IRepository<Device> deviceRepository, IRepository<Eatery> eateryRepository)
    {
        _deviceRepository = deviceRepository;
        _eateryRepository = eateryRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Device>>> GetAll()
    {
        return await _deviceRepository.GetAllAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Device>> GetById(Guid id)
    {
        var result = await _deviceRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Device not found");
        
        return result;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] DeviceDto dto)
    {
        var eatery = await _eateryRepository.GetByIdAsync(dto.EateryId);
        
        if (eatery is null)
            return BadRequest("Eatery not found");
        
        var device = new Device
        {
            SerialNumber = dto.SerialNumber,
            Model = dto.Model,
            Eatery = eatery
        };
        
        await _deviceRepository.CreateAsync(device);
        
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Device device)
    {
        if (id != device.Id)
            return BadRequest("Device ID mismatch");

        await _deviceRepository.UpdateAsync(device);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deviceRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}