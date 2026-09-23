using System.ComponentModel.DataAnnotations;

namespace ToolTrack.DTOs;

public class UpdateToolDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1000000)]
    public decimal Price { get; set; }

    [Range(0, 100000)]
    public int Quantity { get; set; }
}