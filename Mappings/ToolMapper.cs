using ToolTrack.DTOs;
using ToolTrack.Model;

namespace ToolTrack.Mappings;

public static class ToolMapper
{
    public static ToolDto ToDto(Tool tool)
    {
        return new ToolDto
        {
            Id = tool.Id,
            Name = tool.Name,
            Price = tool.Price,
            Quantity = tool.Quantity
        };
    }

    public static Tool ToEntity(CreateToolDto dto)
    {
        return new Tool
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity
        };
    }
}