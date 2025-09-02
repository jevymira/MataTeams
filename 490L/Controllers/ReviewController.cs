using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _490L.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly ReviewContext _context;

    public ReviewController(ReviewContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Review>> GetReviews(string? userId)
    {
        return await _context.Reviews
            .Where(e => e.UserId == userId || userId == null)
            .ToListAsync();
    }
}