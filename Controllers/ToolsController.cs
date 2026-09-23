using Microsoft.AspNetCore.Mvc;
using ToolTrack.Mappings;
using ToolTrack.DTOs;
using ToolTrack.Model;
using ToolTrack.Services;

namespace ToolTrack2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly IToolService _service;

    public ToolsController(IToolService service)
    {
        _service = service;
    }

    [HttpGet]
    /*public async Task<ActionResult<List<ToolDto>>> GetAll()
    {
        var tools = await _service.GetAllAsync();

        var result = tools.Select(tool => new ToolDto
        {
            Id = tool.Id,
            Name = tool.Name,
            Price = tool.Price,
            Quantity = tool.Quantity
        }).ToList();


        return Ok(result);
    }*/
    public async Task<ActionResult<List<ToolDto>>> GetAll()
    {
        var tools = await _service.GetAllAsync();

        var result = tools
            .Select(ToolMapper.ToDto)
            .ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    /*public async Task<ActionResult<ToolDto>> GetById(int id)
    {
        var tool = await _service.GetByIdAsync(id);
        
        var result = new ToolDto
        {
            Id = tool.Id,
            Name = tool.Name,
            Price = tool.Price,
            Quantity = tool.Quantity
        };

        return Ok(result);
    }*/
    public async Task<ActionResult<ToolDto>> GetById(int id)
    {
        var tool = await _service.GetByIdAsync(id);

        var result = ToolMapper.ToDto(tool);

        return Ok(result);
    }

    [HttpPost]
    /*public async Task<ActionResult<Tool>> Create(CreateToolDto dto)
    {
        var tool = new Tool
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity
        };

        var createdTool = await _service.CreateAsync(tool);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTool.Id },
            createdTool
        );
    }*/
    public async Task<ActionResult<ToolDto>> Create(CreateToolDto dto)
    {
        var tool = ToolMapper.ToEntity(dto);

        var createdTool = await _service.CreateAsync(tool);

        var result = ToolMapper.ToDto(createdTool);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTool.Id },
            result
        );
    }


    [HttpPut("{id}")]
    /*public async Task<IActionResult> Update(
        int id,
        CreateToolDto dto)
    {
        var tool = await _service.GetByIdAsync(id);

        tool.Name = dto.Name;
        tool.Price = dto.Price;
        tool.Quantity = dto.Quantity;

        await _service.UpdateAsync(tool);

        return NoContent();
    }*/
    public async Task<IActionResult> Update(
    int id,
    UpdateToolDto dto)
    {
        var tool = await _service.GetByIdAsync(id);

        tool.Name = dto.Name;
        tool.Price = dto.Price;
        tool.Quantity = dto.Quantity;

        await _service.UpdateAsync(tool);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}