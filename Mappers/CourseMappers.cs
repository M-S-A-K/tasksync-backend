using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Mappers;

public static class CourseMappers
{
    public static CourseDto ToCourseDto(this Course courseModel)
    {
        return new CourseDto
        {
            Id = courseModel.Id,
            Name = courseModel.Name,
            ColorCode = courseModel.ColorCode,
            Tasks = courseModel.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Deadline = t.Deadline,
                IsCompleted = t.IsCompleted,
                CourseId = t.CourseId
            }).ToList()
        };
    }

    public static Course ToCourseFromCreateDTO(this CreateCourseRequestDto courseDto)
    {
        return new Course
        {
            Name = courseDto.Name,
            ColorCode = courseDto.ColorCode
        };
    }
}