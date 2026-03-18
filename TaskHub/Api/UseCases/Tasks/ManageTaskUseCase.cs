using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class ManageTaskUseCase : IManageTaskUseCase
{
    private readonly ITaskService _taskService;

    public ManageTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskResponse> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateTaskAsync(title, userId, cancellationToken);
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            UserId = task.CreatedByUserId,
            CreatedAt = task.CreatedUtc.DateTime
        };
    }

    public async Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllTasksAsync(cancellationToken);

        var response = tasks
            .Select(x => new TaskResponse
            {
                Id = x.Id,
                Title = x.Title,
                UserId = x.CreatedByUserId,
                CreatedAt = x.CreatedUtc.DateTime
            })
            .ToList();

        return new TaskListResponse
        {
            Tasks = response,
            TotalCount = response.Count
        };
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId, cancellationToken);

        if (task is null)
        {
            return null;
        }

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            UserId = task.CreatedByUserId,
            CreatedAt = task.CreatedUtc.DateTime
        };
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTaskByIdAsync(taskId, cancellationToken);
    }

    public async Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(taskId, title, cancellationToken);
    }
}