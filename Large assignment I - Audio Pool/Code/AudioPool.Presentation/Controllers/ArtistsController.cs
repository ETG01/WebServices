using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Service.Interfaces;
using AudioPool.Presentation.Attributes;
namespace AudioPool.Presentation.Controllers;



[Analytics]
[Route("api/artists")]
public class ArtistsController : ControllerBase
{ 
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }
    
//Get artists
// htpp://localhost/5001/api/artists
    /// <summary>
    /// Get artists
    /// </summary>
    /// <remarks>
    /// Fetches a list of artists in descending order of their starting date.
    /// An endpoint that fetches all artists with a default limit of 25 items per page. 
    /// The number of items per page can be modified using the pageSize query parameter, 
    /// and different pages can be accessed using the pageNumber query parameter.
    /// </remarks>
    /// <param name="pageNumber">The page number to retrieve; defaults to 1.</param>
    /// <param name="pageSize">The number of artists to retrieve per page; defaults to 25.</param>
    /// <returns>A list of artists.</returns>
    /// <response code="200">Returns a paginated list of artists.</response>
    [Analytics]
    [HttpGet]
    [Route("")]
    public IActionResult GetAllArtists(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 25)
    {
        var artists = _artistService.GetAllArtists(pageNumber, pageSize);
        return Ok(artists);
    }
    
// Get artist by id
// htpp://localhost/5001/api/artists/{id}
    /// <summary>
    /// Get artist by id
    /// </summary>
    /// <remarks>
    /// Fetches an artist by their ID.
    /// An endpoint that retrieves an artist based on the specified ID. 
    /// The ID is specified dynamically in the URL. 
    /// For example, accessing /api/artists/2 will fetch the artist with ID 2.
    /// </remarks>
    /// <param name="id">The ID of the artist to retrieve.</param>
    /// <returns>The artist matching the specified ID, or null if no artist matches the ID.</returns>
    /// <response code="200">Returns the artist corresponding to the specific ID.</response>
    /// <response code="404">No artist was found with the specified ID.</response>
    [Analytics]
    [HttpGet]
    [Route("{artistsId:int}", Name = "GetArtistById")]
    public IActionResult GetArtistsById(int artistsId)
    {
        var artist = _artistService.GetArtistById(artistsId);
        if (artist == null) { return NotFound($"Artist with id {artistsId} was not found."); }
        return Ok(artist);
    }
    
// Get artist albums
// htpp://localhost/5001/api/artists/{id}/albums
    /// <summary>
    /// Get artist albums
    /// </summary>
    /// <remarks>
    /// Fetches all albums associated with a specific artist.
    /// An endpoint that retrieves all albums connected to the artist specified by the ID in the URL. 
    /// For example, accessing /api/artists/5/albums will retrieve all albums connected to the artist with ID 5.
    /// </remarks>
    /// <param name="id">The ID of the artist whose albums are to be retrieved.</param>
    /// <returns>A list of albums associated with the specified artist ID.</returns>
    /// <response code="200">Returns the list of albums for the specific artist ID.</response>
    /// <response code="404">No artist was found with the specified ID, or the artist has no albums.</response>
    [Analytics]
    [HttpGet]
    [Route("{id:int}/albums", Name = "GetAlbumsByArtistsId")]
    public IActionResult GetAlbumsByArtistsId(int id)
    {
        var artist = _artistService.GetAlbumsByArtistsId(id);
        if (artist == null) { return NotFound($"Artist with id {id} was not found."); }
        return Ok(artist);
    }
    
    /// <summary>
    /// Creates a new artist. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint that allows the creation of a new artist record. The artist details should be provided as a JSON object in the request body.
    /// </remarks>
    /// <param name="artistInputModel">A JSON object representing the artist to be created.</param>
    /// <returns>The created artist record.</returns>
    /// <response code="201">Returns the newly created artist.</response>
    /// <response code="400">If the input model is null or invalid.</response>
    [ApiTokenAuthorize]
    [HttpPost]
    [Route("")]
    public IActionResult CreateNewArtist([FromBody] ArtistInputModel artist)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var id = _artistService.CreateNewArtist(artist);
        
        return CreatedAtRoute("GetArtistById", new { artistsId = id }, artist);
    }

    /// <summary>
    /// Updates an existing artist. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint that allows the updating of an existing artist record using the artist ID specified in the URL. The updated details should be provided as a JSON object in the request body.
    /// </remarks>
    /// <param name="id">The ID of the artist to be updated.</param>
    /// <param name="artistInputModel">A JSON object representing the updated details of the artist.</param>
    /// <returns>The updated artist record.</returns>
    /// <response code="200">Returns the updated artist.</response>
    /// <response code="400">If the input model is null or invalid.</response>
    /// <response code="404">If an artist with the specified ID does not exist.</response>
    [ApiTokenAuthorize]
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateArtist(int id, [FromBody] ArtistInputModel artist)
    {
        var existingArtist = _artistService.GetArtistById(id);
        if (existingArtist == null) { return NotFound($"Artist with id {id} was not found."); }

        _artistService.UpdateArtist(id, artist);
        return NoContent();
    }
    
    /// <summary>
    /// Links an artist to a genre. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint that associates a specified artist with a specified genre using their respective IDs provided in the URL.
    /// </remarks>
    /// <param name="artistId">The ID of the artist to be linked.</param>
    /// <param name="genreId">The ID of the genre to which the artist will be linked.</param>
    /// <returns>An indication of whether the linking was successful.</returns>
    /// <response code="200">The artist was successfully linked to the genre.</response>
    /// <response code="404">Artist or genre not found.</response>
    [ApiTokenAuthorize]
    [HttpPost]
    [Route("{artistId:int}/genres/{genreId:int}")]
    public IActionResult LinkArtistToGenre(int artistId, int genreId)
    {
        try
        {
            _artistService.LinkArtistToGenre(artistId, genreId);
            return Ok("Artist linked to genre");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Adjust error handling as necessary
        }
    }

    
}



