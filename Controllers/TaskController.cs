using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers;

[Route("api/task")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("{courseId:int}")]
    public async Task<IActionResult> CreateTask([FromRoute] int courseId, [FromBody] CreateTaskRequestDto taskDto)
    {
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);
        if (!courseExists) return NotFound("Course not found"); 

        var taskModel = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            Deadline = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            CourseId = courseId 
        };

        await _context.Tasks.AddAsync(taskModel);
        await _context.SaveChangesAsync();

        return Ok(new TaskDto {
            Id = taskModel.Id, Title = taskModel.Title, Description = taskModel.Description,
            Deadline = taskModel.Deadline, IsCompleted = taskModel.IsCompleted, CourseId = taskModel.CourseId
        });
    }

    [HttpPut("toggle/{id:int}")]
    public async Task<IActionResult> ToggleComplete([FromRoute] int id)
    {
        var taskItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (taskItem == null) return NotFound("Task nahi mila!");

        taskItem.IsCompleted = !taskItem.IsCompleted; 
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask([FromRoute] int id)
    {
        var taskItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (taskItem == null) return NotFound("Task nahi mila!");

        _context.Tasks.Remove(taskItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}