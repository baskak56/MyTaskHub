using Dal.Entities;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;

namespace Logic.Tasks.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken)
    {
        var taskEntity = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = userId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        var task = await _taskRepository.CreateAsync(taskEntity, cancellationToken);

        return new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        var result = tasks
            .Select(x => new TaskModel
            {
                Id = x.Id,
                Title = x.Title,
                CreatedByUserId = x.CreatedByUserId,
                CreatedUtc = x.CreatedUtc
            })
            .ToList()
            .AsReadOnly();

        return result;
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);

        if (task == null)
        {
            return null;
        }

        return new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }

    public async Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _taskRepository.UpdateTitleAsync(taskId, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteByIdAsync(taskId, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllAsync(cancellationToken);
    }
}