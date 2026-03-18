namespace Api.Controllers.Tasks.Request;

public class CreateTaskRequest
{
	public string Title { get; set; } = string.Empty;
	public Guid UserId { get; set; }
}