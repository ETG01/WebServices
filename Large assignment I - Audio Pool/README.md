# Large Assignment I - Audio Pool

In this assignment, we were tasked with creating a RESTful Web API using .NET for a company named "Audio Pool." The company aims to provide music to the public through a structured and fast-working API. The API should adhere to high REST standards to ensure clean and crisp data delivery to users.

## Setup Instructions

### Prerequisites

- .NET SDK 7.0 or later
- A text editor or an IDE (like Visual Studio or JetBrains Rider)
- SQLite (as the project uses an SQLite database)

### Steps to Set Up the Project

1. **Clone the Repository**
   
   ```sh
   git clone https://github.com/ETG01/AudioPool.git

2. **Navigate to the Project Directory**
   
   ```sh
   cd AudioPool

3. **Install the Necessary NuGet Packages**
   
   ```sh
   dotnet restore
4. **Setup the Database**
    
    The project uses SQLite as its database. The connection string is configured in the appsettings.json file as follows:
    
    ```json
    "ConnectionStrings": {
        "AudioPoolDatabase": "Data Source=DbAudioPool.sqlite"
    }
    ```
    Ensure that the SQLite database file (DbAudioPool.sqlite) is present in the AudioPool.Presentation directory.
5. **Run the Application**
    
    Navigate to the AudioPool.Presentation directory and run the following command to start the application:
    ```sh
    dotnet run
    ```
    The application will start and you can access it via a web browser. For example to access the Swagger UI, navigate to the following URL:
    ```sh
   http://localhost:5143/swagger/index.html
   ```
   with your port my port is 5143 for example.

## API Functionalities
The API provides a range of functionalities categorized into unauthorized and authorized routes. Here are the general rules that apply to all endpoints:

- Suitable HTTP status codes should be returned to indicate various states such as resource not found, client error, or server error.
- Data sent to the server using POST or PUT should be in JSON format within the request body.
- HATEOAS should be honored for better navigation, especially when a resource is created.
- Responses utilizing HATEOAS should include a links property encompassing rel and href to describe the relationship and the anchor itself.

## Unauthorized Routes

#### Genres
- **Get all genres**
  - Endpoint: `/api/genres`
  - Method: GET
  - Description: Fetches all genres. The response includes details such as genre ID, name, and links to self and associated artists.

- **Get genre by ID**
  - Endpoint: `/api/genres/{id}`
  - Method: GET
  - Description: Fetches a genre by its ID, provided dynamically in the URL parameter. The response includes details such as genre ID, name, number of artists, and links to self and associated artists.

#### Artists
- **Get all artists**
  - Endpoint: `/api/artists`
  - Method: GET
  - Description: Fetches all artists with a default limit of 25 items per page. The `pageSize` and `pageNumber` query parameters can be used to modify the pagination. Artists are ordered by descending start date.

- **Get artist by ID**
  - Endpoint: `/api/artists/{id}`
  - Method: GET
  - Description: Fetches an artist by its ID, provided dynamically in the URL parameter. The response includes detailed information about the artist, including links to self, edit, delete, albums, and associated genres.

- **Get artist albums**
  - Endpoint: `/api/artists/{id}/albums`
  - Method: GET
  - Description: Fetches all albums associated with a specific artist, identified by the artist ID provided dynamically in the URL parameter.

#### Albums
- **Get album by ID**
  - Endpoint: `/api/albums/{id}`
  - Method: GET
  - Description: Fetches an album by its ID, provided dynamically in the URL parameter. The response includes detailed information about the album, including links to self, delete, songs, and associated artists.

- **Get songs on album**
  - Endpoint: `/api/albums/{id}/songs`
  - Method: GET
  - Description: Fetches all songs associated with a specific album, identified by the album ID provided dynamically in the URL parameter.

#### Songs
- **Get song by ID**
  - Endpoint: `/api/songs/{id}`
  - Method: GET
  - Description: Fetches a song by its ID, provided dynamically in the URL parameter. The response includes detailed information about the song, including links to self, edit, delete, and the associated album.


## Attribute

To ensure secure and authorized access to the authorized routes, a custom attribute is implemented. This attribute performs the following functions:

1. It restricts access to decorated routes, allowing access only when a valid token is provided.
2. The token must be stored in an HTTP header named `api-token`.
3. The valid API token is a hard-coded value which is "69", and it is validated with each authenticated request.


## Authorized Routes (API-Token = 69)

#### Genres
- **Create a genre**
  - Endpoint: `/api/genres`
  - Method: POST
  - Description: Creates a new genre. A JSON request body should be sent in the form of the `GenreInputModel`.

#### Artists
- **Create an artist**
  - Endpoint: `/api/artists`
  - Method: POST
  - Description: Creates a new artist. A JSON request body should be sent in the form of the `ArtistInputModel`.
- **Update an artist**
  - Endpoint: `/api/artists/{id}`
  - Method: PUT
  - Description: Updates an artist. A dynamic value for the artist ID should be provided in the URL parameter. The request body should be a JSON represented as `ArtistInputModel`.
- **Link artist to genre**
  - Endpoint: `/api/artists/{artistId}/genres/{genreId}`
  - Method: POST
  - Description: Links an artist to a genre using dynamic values for artist ID and genre ID in the URL parameters.

#### Albums
- **Create an album**
  - Endpoint: `/api/albums`
  - Method: POST
  - Description: Creates a new album. A JSON request body should be sent in the form of the `AlbumInputModel`.
- **Delete an album**
  - Endpoint: `/api/albums/{id}`
  - Method: DELETE
  - Description: Deletes an album using a dynamic value for the album ID in the URL parameter.

#### Songs
- **Delete a song**
  - Endpoint: `/api/songs/{id}`
  - Method: DELETE
  - Description: Deletes a song using a dynamic value for the song ID in the URL parameter.
- **Update a song**
  - Endpoint: `/api/songs/{id}`
  - Method: PUT
  - Description: Updates a song using a dynamic value for the song ID in the URL parameter.
- **Add a song**
  - Endpoint: `/api/songs`
  - Method: POST
  - Description: Creates a new song. A JSON request body should be sent in the form of the `SongInputModel`.


## Folder Structures Layers

### Presentation
```
AudioPool.Presentation
│
├─ Program.cs
├─ DbAudioPool.sqlite
├─ appsettings.Development.json
├─ appsettings.json
│
├─ Attributes
│   ├─ AnalyticsAttribute.cs
│   └─ ApiTokenAuthorizeAttribute.cs
│
├─ Controllers
│   ├─ AlbumsController.cs
│   ├─ ArtistsController.cs
│   ├─ GenresController.cs
│   └─ SongsController.cs
│
├─ Filters
│   └─ TimeSpanSchemaFilter.cs
│
└─ Profiles
    └─ MappingProfile.cs

```

### Repository
```
AudioPool.Repository
│
├─ Data
│   └─ AudioPoolDbContext.cs
│
├─ Implementations
│   ├─ AlbumRepository.cs
│   ├─ ArtistRepository.cs
│   ├─ GenresRepository.cs
│   └─ SongsRepository.cs
│
└─ Interfaces
    ├─ IAlbumRepository.cs
    ├─ IArtistRepository.cs
    ├─ IGenresRepository.cs
    └─ ISongsRepository.cs
```
### Models
```
AudioPool.Models
│
├─ DTOs
│   ├─ AlbumDetailsDto.cs
│   ├─ AlbumDto.cs
│   ├─ ArtistDetailsDto.cs
│   ├─ ArtistDto.cs
│   ├─ GenreDetailsDto.cs
│   ├─ GenreDto.cs
│   ├─ SongDetailsDto.cs
│   └─ SongDto.cs
│
├─ Entities
│   ├─ Album.cs
│   ├─ AlbumArtist.cs
│   ├─ Artist.cs
│   ├─ ArtistGenre.cs
│   ├─ Genre.cs
│   └─ Song.cs
│
├─ InputModels
│   ├─ AlbumInputModel.cs
│   ├─ ArtistInputModel.cs
│   ├─ GenreInputModel.cs
│   └─ SongInputModel.cs
│
├─ Envelope.cs
├─ HyperMediaExtensions.cs
├─ HyperMediaModel.cs
└─ LinkRepresentation.cs

```
### Service
```
AudioPool.Service
│
├─ Interfaces
│   ├─ IAlbumService.cs
│   ├─ IArtistService.cs
│   ├─ IGenresService.cs
│   └─ ISongsService.cs
│
└─ Services
    ├─ AlbumService.cs
    ├─ ArtistService.cs
    ├─ GenresService.cs
    └─ SongsService.cs

```


## Development
The Program.cs file in the AudioPool.Presentation directory is the entry point of the application. It configures services, middleware, and sets up the application's HTTP request pipeline.

For further development, you can explore the various layers of the application including Models, Repository, Service, and Presentation layers, each having its respective functionalities and responsibilities.

## Authors
- **Einar Tómas Grétarsson**
- **Sigurður Valur Ásbjarnarson**

### Institution
Reykjavik University

### Course
Web Services (T-514-VEFT)

