using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.DTOs;

public class CreateCourseRequestDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Course name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;

    public string ColorCode { get; set; } = "#00BFFF";
}