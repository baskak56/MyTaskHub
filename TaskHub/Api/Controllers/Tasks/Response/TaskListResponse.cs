namespace Api.Controllers.Tasks.Response;

public class TaskListResponse
{
    public List<TaskResponse> Tasks { get; set; } = new();
    public int TotalCount { get; set; }
}