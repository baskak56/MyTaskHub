using Dal.Entities;

namespace Dal.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken);
    Task<IReadOnlyList<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateTitleAsync(Guid id, string title, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
}