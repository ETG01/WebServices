using AudioPool.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.Presentation.Attributes;
namespace AudioPool.Presentation.Controllers;

[ApiController]
[Route("api/songs")]
public class SongsController : ControllerBase
{
    private readonly ISongsService _SongsService;

    public SongsController(ISongsService songsService)
    {
        _SongsService = songsService;
    }

    // htpp://localhost/5001/api/songs/{id}
    /// <summary>
    /// Get all songs by Genre ID.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves a list of songs associated with a specific Genre based on its ID.
    /// </remarks>
    /// <param name="genreId">The ID of the Genre to retrieve songs for.</param>
    /// <returns>
    /// A list of songs belonging to the specified Genre.
    /// </returns>
    /// <response code="200">Returns the list of songs for the specified Genre.</response>
    /// <response code="404">No Genre was found with the specified ID.</response>
    [HttpGet]
    [Route("{songsId:int}", Name = "GetSongsById")]
    public IActionResult GetSongsById(int songsId)
    {
        var song = _SongsService.GetSongById(songsId);
        if (song == null)
        {
            return NotFound($"Song with the id: {songsId} was not found!");
        }
        return Ok(song);
    }
    
    /// <summary>
    /// Delete a song. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint (/api/songs/{id}) which deletes a song. A URL parameter should
    /// be used to provide a dynamic value for the song id, e.g. /api/songs/9 would
    /// delete a song with the id 9.
    /// </remarks>
    /// <param name="id">The ID of the song to delete.</param>
    /// <returns>
    /// A message indicating the outcome of the deletion operation.
    /// </returns>
    /// <response code="200">The song was successfully deleted, and a message indicating the successful deletion is returned.</response>
    /// <response code="404">No song was found with the specified ID.</response>
    [ApiTokenAuthorize]
    [HttpDelete]
    [Route("{id:int}", Name = "DeleteSong")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var result = await _SongsService.DeleteSong(id);
        if (result)
        {
            return Ok($"Song with id={id} deleted");
        }
        return NotFound();
    }
    
    /// <summary>
    /// Update a song. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint (/api/songs/{id}) which updates a song. A URL parameter should be used to provide a dynamic value for the song id, e.g. /api/songs/9 would update a song with the id 9. 
    /// The body of the request should contain the updated song details.
    /// </remarks>
    /// <param name="id">The ID of the song to update.</param>
    /// <param name="songInputModel">The updated details of the song as a JSON object in the request body.</param>
    /// <returns>
    /// A message indicating the outcome of the update operation.
    /// </returns>
    /// <response code="200">The song was successfully updated, and a message indicating the successful update is returned.</response>
    /// <response code="404">No song was found with the specified ID.</response>
    [ApiTokenAuthorize]
    [HttpPut]
    [Route("{id:int}", Name = "UpdateSong")]
    public IActionResult UpdateSong(int id, [FromBody] SongInputModel songInputModel)
    {
        bool isUpdated = _SongsService.UpdateSong(id, songInputModel);
        if (isUpdated)
        {
            return Ok("Song updated successfully");
        }
        return NotFound("Song not found");
    }
    
    /// <summary>
    /// Add a song. api-token = 69 required for this endpoint.
    /// </summary>
    /// <remarks>
    /// An endpoint (/api/songs) which creates a new song. A request body represented as JSON, conforming to the SongInputModel schema, should be sent with the request to provide the details of the song to be created.
    /// </remarks>
    /// <param name="songInputModel">The details of the song to create, represented as a JSON object in the request body.</param>
    /// <returns>
    /// The details of the created song along with the URI where the newly created song can be found.
    /// </returns>
    /// <response code="201">The song was successfully created and the details are returned along with the URI of the new song.</response>
    /// <response code="400">The request body is null or invalid.</response>
    [ApiTokenAuthorize]
    [HttpPost]
    [Route("", Name = "CreateSong")]
    public IActionResult CreateSong([FromBody] SongInputModel songInputModel)
    {
        if (songInputModel == null)
        {
            return BadRequest("Song model is null");
        }

        var songId = _SongsService.CreateSong(songInputModel);
        return CreatedAtRoute("GetSongsById", new { songsId = songId }, songInputModel);
    }

}