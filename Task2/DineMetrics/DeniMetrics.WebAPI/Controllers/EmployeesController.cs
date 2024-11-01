﻿using DineMetrics.Core.Dto;
using DineMetrics.Core.Models;
using DineMetrics.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class EmployeesController : BaseController
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeesController(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        return await _employeeRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(Guid id)
    {
        var result = await _employeeRepository.GetByIdAsync(id);

        if (result is null)
            return BadRequest("Employee not found");
        
        return result;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EmployeeDto dto)
    {
        var employee = new Employee
        {
            // Map properties from dto to model
        };

        await _employeeRepository.CreateAsync(employee);

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Employee employee)
    {
        if (id != employee.Id)
            return BadRequest("Employee ID mismatch");

        await _employeeRepository.UpdateAsync(employee);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _employeeRepository.RemoveByIdAsync(id);
        
        return Ok();
    }
}
