using ToolTrack.Model;

namespace ToolTrack.Repositories;

public interface IToolRepository
{
    Task<List<Tool>> GetAllAsync();

    Task<Tool?> GetByIdAsync(int id);

    Task AddAsync(Tool tool);

    void Update(Tool tool);

    void Delete(Tool tool);

    Task SaveChangesAsync();
}