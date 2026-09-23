using Microsoft.EntityFrameworkCore;
using ToolTrack.Data;
using ToolTrack.Model;

namespace ToolTrack.Repositories;

public class ToolRepository : IToolRepository
{
    private readonly AppDbContext _dbContext;

    public ToolRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Tool>> GetAllAsync()
    {
        return await _dbContext.Tools.ToListAsync();
    }

    public async Task<Tool?> GetByIdAsync(int id)
    {
        return await _dbContext.Tools.FindAsync(id);
    }

    public async Task AddAsync(Tool tool)
    {
        await _dbContext.Tools.AddAsync(tool);
    }

    public void Update(Tool tool)
    {
        _dbContext.Tools.Update(tool);
    }

    public void Delete(Tool tool)
    {
        _dbContext.Tools.Remove(tool);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}