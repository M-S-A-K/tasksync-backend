namespace TaskManagerAPI.Models;

public class Course
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; } = "#00BFFF";
        public List<TaskItem> Tasks { get; set; } = new();
}