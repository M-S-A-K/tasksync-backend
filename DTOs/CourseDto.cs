namespace TaskManagerAPI.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ColorCode { get; set; } = "#00BFFF";
    public List<TaskDto> Tasks { get; set; } = new(); // Ab loop nahi banega
}