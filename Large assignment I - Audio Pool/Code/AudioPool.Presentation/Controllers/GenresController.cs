using AudioPool.Service;
using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AudioPool.Presentation.Attributes;
namespace AudioPool.Presentation.Controllers;

[ApiController]
[Route("api")]
public class GenresController : ControllerBase
{
    private readonly IGenresService _genresService;

    public GenresController(IGenresService genresService)
    {
        _genresService = genresService;
    }
    // ================================== GET ==========================================
    // htpp://localhost/5001/api/genres
    /// <summary>
    /// Get all Genres.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves a list of all genres available in the system.
    /// </remarks>
    /// <returns>
    /// A list of genres.
    /// </returns>
    /// <response code="200">Returns the list of genres.</response>
    [HttpGet]
    [Route("genres")]
    public IActionResult GetAllGenres()
    {
        var genres = _genresService.GetAllGenres();
        return Ok(genres);
    }
    
    // htpp://localhost/5001/api/genres/{id}
    /// <summary>
    /// Get a Genre by ID.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves a specific genre based on its unique ID.
    /// </remarks>
    /// <param name="genreId">The ID of the genre to retrieve.</param>
    /// <returns>
    /// The genre matching the specified ID, or a 404 response if the genre is not found.
    /// </returns>
    /// <response code="200">Returns the genre corresponding to the specified ID.</response>
    /// <response code="404">No genre was found with the specified ID.</response>
    [HttpGet]
    [Route("genres/{genreId:int}", Name = "GetGenresById")]
    public IActionResult GetGenresById(int genreId)
    {
        var genre = _genresService.GetGenresById(genreId);
        if (genre == null)
        {
            return NotFound($"Genre with the id: {genreId} was not found!");
        }
        return Ok(genre);
    }
    // ================================== POST ==========================================
    // htpp://localhost/5001/api/genres 
    /// <summary>
    /// Create a New Genre. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// This endpoint allows you to create a new genre by providing the necessary information in the request body.
    /// The created genre will be returned with its unique ID.
    /// </remarks>
    /// <param name="genreInp">The information for the new genre to be created.</param>
    /// <returns>
    /// A 201 response with the newly created genre and a location header pointing to the URI of the created genre.
    /// </returns>
    /// <response code="201">Returns the newly created genre with its unique ID.</response>
    /// <response code="400">If the request body is invalid, it returns a 400 Bad Request response.</response>
    [ApiTokenAuthorize]
    [HttpPost]
    [Route("genres")]
    public IActionResult CreateGenre([FromBody] GenreInputModel genreInp)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var id = _genresService.CreateGenre(genreInp);
        
        return CreatedAtRoute("GetGenresById", new { GenreId = id }, genreInp);
    }
}