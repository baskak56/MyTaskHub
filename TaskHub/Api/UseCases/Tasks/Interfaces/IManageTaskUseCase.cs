using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

public interface IManageTaskUseCase
{
    Task<TaskResponse> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken);
    Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken);
    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);
    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);
    Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);
}