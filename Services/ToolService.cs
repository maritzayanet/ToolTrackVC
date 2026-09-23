using ToolTrack.Exceptions;
using ToolTrack.Model;
using ToolTrack.Repositories;

namespace ToolTrack.Services;

public class ToolService : IToolService
{
    private readonly IToolRepository _repository;

    private readonly ILogger<ToolService> _logger;

    public ToolService(
    IToolRepository repository,
    ILogger<ToolService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Tool>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Tool> GetByIdAsync(int id)
    {
        _logger.LogInformation(
            "Buscando herramienta con ID {ToolId}.",
            id
        );

        var tool = await _repository.GetByIdAsync(id);

        if (tool == null)
        {
            _logger.LogWarning(
                "No se encontró la herramienta con ID {ToolId}.",
                id
            );

            throw new ToolNotFoundException(
                $"La herramienta con ID {id} no existe."
            );
        }

        return tool;
    }

    public async Task<Tool> CreateAsync(Tool tool)
    {
        _logger.LogInformation(
            "Creando herramienta {ToolName}.",
            tool.Name
        );

        await _repository.AddAsync(tool);
        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Herramienta creada correctamente. ID: {ToolId}.",
            tool.Id
        );

        return tool;
    }

    public async Task UpdateAsync(Tool tool)
    {
        _repository.Update(tool);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation(
            "Eliminando herramienta con ID {ToolId}.",
            id
        );

        var tool = await _repository.GetByIdAsync(id);

        if (tool == null)
        {
            _logger.LogWarning(
                "No se puede eliminar la herramienta {ToolId} porque no existe.",
                id
            );

            throw new ToolNotFoundException(
                $"La herramienta con ID {id} no existe."
            );
        }

        _repository.Delete(tool);

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Herramienta {ToolId} eliminada correctamente.",
            id
        );
    }
}