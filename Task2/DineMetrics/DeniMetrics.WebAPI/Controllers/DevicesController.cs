using DineMetrics.Core.Dto;
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
    public async Task<ActionResult<List<DeviceDto>>> GetAll()
    {
        var devices = await _deviceRepository.GetAllAsync();
        
        var devicesDtos = devices.Select(device => new DeviceDto
        {
            SerialNumber = device.SerialNumber,
            Model = device.Model,
            EateryId = device.Eatery.Id
        }).ToList();

        return devicesDtos;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceDto>> GetById(Guid id)
    {
        var result = await _deviceRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Device not found");
        
        return new DeviceDto
        {
            SerialNumber = result.SerialNumber,
            Model = result.Model,
            EateryId = result.Eatery.Id
        };
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
        
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] DeviceDto dto)
    {
        var existingDevice = await _deviceRepository.GetByIdAsync(id);

        if (existingDevice == null)
            return BadRequest("Device not found");

        var eatery = await _eateryRepository.GetByIdAsync(dto.EateryId);
        if (eatery == null)
            return BadRequest("Eatery not found");

        existingDevice.SerialNumber = dto.SerialNumber;
        existingDevice.Model = dto.Model;
        existingDevice.Eatery = eatery;

        await _deviceRepository.UpdateAsync(existingDevice);

        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deviceRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}