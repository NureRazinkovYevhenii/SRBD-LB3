using Microsoft.AspNetCore.Mvc;
using SRBD_LB3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRBD_LB3.Responses;
using SRBD_LB3.Requests;

namespace SRBD_LB3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("films")]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _context.Films
                .Select(f => new FilmResponse
                {
                    Id = f.FilmId,
                    Name = f.Name,
                    ImageUrl = f.ImageUrl,
                    ReleaseYear = f.ReleaseDate.Year,
                    Rating = f.Rating
                })
                .ToListAsync();

            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedFilmResponse>> GetFilmDetails(int id)
        {
            var movie = await _context.Films
                .Include(m => m.Author)
                .Include(m => m.Company)
                .Include(m => m.Screenings)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (movie == null)
            {
                return NotFound();
            }

            var filmResponse = new DetailedFilmResponse
            {
                Name = movie.Name,
                ReleaseYear = movie.ReleaseDate.Year,
                Description = movie.Description ?? "No description available",
                WatchCount = movie.WatchCount,
                Price = movie.Price,
                Country = movie.Country,
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating,
                AuthorName = movie.Author.FirstName + ' ' + movie.Author.LastName,
                CompanyName = movie.Company.CompanyName,
                Screenings = movie.Screenings.Select(s => new ScreeningResponse
                {
                    ScreeningDate = s.ScreeningDate
                }).ToList()
            };

            return Ok(filmResponse);
        }
    }
}
