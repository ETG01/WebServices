using Microsoft.AspNetCore.Mvc;
using AudioPool.Service.Interfaces;
using AudioPool.Presentation.Attributes;
using AudioPool.Models.InputModels;

namespace AudioPool.Presentation.Controllers
{
    [Analytics]
    [Route("api/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        /// <summary>
        /// Get album by ID
        /// </summary>
        /// <remarks>
        /// Fetches an album by its ID.
        /// An endpoint that retrieves an album based on the specified ID. 
        /// The ID is specified dynamically in the URL. 
        /// For example, accessing /api/albums/3 will fetch the album with ID 3.
        /// </remarks>
        /// <param name="id">The ID of the album to retrieve.</param>
        /// <returns>The album matching the specified ID, or null if no album matches the ID.</returns>
        /// <response code="200">Returns the album corresponding to the specific ID.</response>
        /// <response code="404">No album was found with the specified ID.</response>
        [HttpGet]
        [Route("{id:int}", Name = "GetAlbumById")]
        public IActionResult GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null) 
            { 
                return NotFound($"Album with id {id} was not found."); 
            }
            return Ok(album);
        }
        
        /// <summary>
        /// Get songs on album
        /// </summary>
        /// <remarks>
        /// Fetches all songs connected to the specified album ID. 
        /// An endpoint that retrieves all songs linked to an album, using a dynamic ID specified in the URL. 
        /// For example, accessing /api/albums/7/songs will fetch all songs connected to the album with ID 7.
        /// </remarks>
        /// <param name="id">The ID of the album to retrieve the songs from.</param>
        /// <returns>A list of songs associated with the specified album ID, or null if no songs are connected to the album.</returns>
        /// <response code="200">Returns the songs connected to the specified album ID.</response>
        /// <response code="404">No album was found with the specified ID, or there are no songs connected to this album.</response>
        [HttpGet]
        [Route("{id:int}/songs", Name = "GetSongsOnAlbum")]
        public IActionResult GetSongsOnAlbum(int id)
        {
            var songs = _albumService.GetSongsOnAlbum(id);
            if (songs == null)
            {
                return NotFound();
            }
            return Ok(songs);
        }
        
        
        /// <summary>
        /// Create an album | api-token = 69 required for this endpoint.
        /// </summary>
        /// <remarks>
        /// Creates a new album with details specified in the request body. 
        /// The request body should be in JSON format, representing the details of the album as per the AlbumInputModel schema.
        /// Example endpoint: POST /api/albums
        /// </remarks>
        /// <param name="albumInputModel">The details of the album to create, represented as a JSON object in the request body.</param>
        /// <returns>The created album details along with the URI where the newly created album can be found.</returns>
        /// <response code="201">The album was successfully created and the details are returned along with the URI of the new album.</response>
        /// <response code="400">The request body is null or invalid.</response>
        [ApiTokenAuthorize]
        [HttpPost]
        [Route("", Name = "CreateAlbum")]
        public IActionResult CreateAlbum([FromBody] AlbumInputModel albumInputModel)
        {
            if (albumInputModel == null)
            {
                return BadRequest("Album model is null");
            }

            var albumId = _albumService.CreateAlbum(albumInputModel);
            return CreatedAtRoute("GetAlbumById", new { id = albumId }, albumInputModel);
        }
        
        /// <summary>
        /// Delete an album | api-token = 69 required for this endpoint.
        /// </summary>
        /// <remarks>
        /// Deletes an existing album specified by the ID in the URL parameter.
        /// Example endpoint: DELETE /api/albums/9
        /// </remarks>
        /// <param name="id">The ID of the album to delete.</param>
        /// <returns>A message indicating the outcome of the deletion operation.</returns>
        /// <response code="200">The album was successfully deleted, and a message indicating the successful deletion is returned.</response>
        /// <response code="404">No album was found with the specified ID.</response>
        [ApiTokenAuthorize]
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteAlbum")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await _albumService.DeleteAlbum(id);
            if (result)
            {
                return Ok($"Album with id={id} deleted");
            }
            return NotFound();
        }
    }

}