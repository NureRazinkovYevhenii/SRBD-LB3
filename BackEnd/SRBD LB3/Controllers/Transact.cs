using Microsoft.AspNetCore.Mvc;
using SRBD_LB3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRBD_LB3.Responses;
using SRBD_LB3.Requests;
using Microsoft.Data.SqlClient;
using System.Dynamic;

namespace SRBD_LB3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("procedure")]
        public async Task<IActionResult> UpdateFilmDescriptions([FromBody] UpdateDescriptionRequest request)
        {
            if (string.IsNullOrEmpty(request.CompanyName))
            {
                return BadRequest("Company name is required.");
            }

            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateFilmDescriptions @CompanyName = {0}",
                    request.CompanyName);

                return result >= 0 ? Ok("Procedure executed successfully.") : StatusCode(500, "Error executing procedure.");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Компания с таким именем не существует."))
                {
                    return BadRequest("No Company with this name.");
                }

                if (ex.Message.Contains("The company with the given name does not exist"))
                {
                    return NotFound("The company with the given name does not exist.");
                }

                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("scalar-function")]
        public async Task<IActionResult> GetAuthorPriceCountryCount([FromQuery] int price, [FromQuery] string country)
        {
            if (price <= 0 || string.IsNullOrEmpty(country))
            {
                return BadRequest("Price and Country are required.");
            }

            var result = await _context.Set<AuthorPriceCountryCountResult>()
        .FromSqlRaw("SELECT dbo.AuthorPriceCountryCount({0}, {1}) AS Value", price, country)
        .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound("No data found.");
            }

            return Ok(result.Value);
        }

        [HttpGet("table-function")]
        public async Task<IActionResult> GetFilmsByAuthorPriceCountry([FromQuery] int price, [FromQuery] string country)
        {
            if (price <= 0 || string.IsNullOrEmpty(country))
            {
                return BadRequest("Price and Country are required.");
            }

            var films = new List<dynamic>();

            // Выполняем raw SQL запрос
            var sqlQuery = "SELECT * FROM dbo.GetFilmsByAuthorPriceCountry(@Price, @Country)";
            var priceParam = new SqlParameter("@Price", price);
            var countryParam = new SqlParameter("@Country", country);

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                command.Parameters.Add(priceParam);
                command.Parameters.Add(countryParam);

                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        dynamic film = new ExpandoObject();
                        film.FilmID = result["FilmID"];
                        film.Name = result["Name"];
                        film.Description = result["Description"];
                        film.Price = result["Price"];
                        film.Author = result["Author"];
                        films.Add(film);
                    }
                }

                _context.Database.CloseConnection();
            }

            if (films == null || films.Count == 0)
            {
                return NotFound("No films found for the given criteria.");
            }

            return Ok(films);
        }
    }
}
