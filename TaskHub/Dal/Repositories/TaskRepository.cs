using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<IReadOnlyList<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task UpdateTitleAsync(Guid id, string title, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync([id], cancellationToken);
        if (task is not null)
        {
            task.Title = title;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync([id], cancellationToken);
        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _context.Tasks.ExecuteDeleteAsync(cancellationToken);
    }
}