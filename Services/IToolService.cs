using ToolTrack.Model;

namespace ToolTrack.Services;

public interface IToolService
{
    Task<List<Tool>> GetAllAsync();

    Task<Tool> GetByIdAsync(int id);

    Task<Tool> CreateAsync(Tool tool);

    Task UpdateAsync(Tool tool);

    Task DeleteAsync(int id);
}