using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Mappers;

namespace TaskManagerAPI.Controllers;

[Route("api/course")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CourseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.Courses .Include(c => c.Tasks).ToListAsync();
        return Ok(courses.Select(s => s.ToCourseDto())); 
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var course = await _context.Courses.Include(c => c.Tasks).FirstOrDefaultAsync(x => x.Id == id);
        if (course == null) return NotFound(); 
        return Ok(course.ToCourseDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequestDto courseDto)
    {
        var courseModel = courseDto.ToCourseFromCreateDTO();
        await _context.Courses.AddAsync(courseModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = courseModel.Id }, courseModel.ToCourseDto());
    }

    [HttpGet("search/{cname}")]
    public async Task<IActionResult> SearchByName([FromRoute] string cname)
    {
        var courses = await _context.Courses
                                    .Include(c => c.Tasks)
                                    .Where(x => x.Name.ToLower().Contains(cname.ToLower()))
                                    .ToListAsync();
        return Ok(courses.Select(s => s.ToCourseDto())); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateCourseRequestDto updateDto)
    {
        var courseModel = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (courseModel == null) return NotFound("Course Does Not Exist!");

        courseModel.Name = updateDto.Name;
        courseModel.ColorCode = updateDto.ColorCode;

        await _context.SaveChangesAsync();
        return Ok(courseModel.ToCourseDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var courseModel = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (courseModel == null) return NotFound("Course Does Not Exist!");

        _context.Courses.Remove(courseModel);
        await _context.SaveChangesAsync();
        return NoContent(); 
    }
}