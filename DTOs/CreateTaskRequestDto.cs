using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.DTOs;

public class CreateTaskRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(7); // Default deadline is 7 days from now
}