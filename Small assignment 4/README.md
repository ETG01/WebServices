# Tokenize API

Tokenize is an API designed to explore and implement token-based authentication. It's a perfect playground to apply your newfound skills in JWT (JSON Web Tokens). The API is structured in a three-layer architecture and uses SQLite for database operations.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Author](#author)
- [Institution](#institution)
- [Course](#course)

## Features
### Account Management
- **Register**: Create a new user account.
- **Login**: Authenticate and receive a JWT token.

### Book Management (Authorized Routes)
- **GetAllBooks**: Retrieve a list of all books.
- **GetBookById**: Get details of a specific book by its ID.
- **CreateBook**: Add a new book to the database.
- **UpdateBookById**: Update details of an existing book.

### JWT Token Service
- **GenerateJWT**: Generate a JWT token with claims like full name, email address, and national ID.

## Technologies Used

- .NET Core 7.0
- Entity Framework Core
- SQLite

## Setup and Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ETG01/Tokenize.git
   ```

2. **Navigate to Project Directory**:
   ```bash
   cd Tokenize.API
   ```

3. **Install the Necessary NuGet Packages**:
   ```bash
   dotnet restore
   ```

4. **Setup the AppSettings**: The project uses SQLite as its database & JwtSettings. The connection string is configured in the appsettings.json file as follows:

    ```json
    "ConnectionStrings": {
    "AuthenticationDbConnectionString": "Data Source=./auth.db"
   },
   "JwtSettings": {
   "Secret": "ThisIsALongSecretKeyThatIsAtLeast32Characters",
   "Issuer": "localhost",
   "Audience": "TokenizerAPP"
   }
    ```

5. **Database Migration**: Apply the latest migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```
6. **Run the Application**:
   ```bash
   dotnet run
   ```
8. **Swagger**:
   #### The application will start and you can access it via a web browser. For example to access the Swagger UI, navigate to the following URL:
    ```sh
   http://localhost:5143/swagger/index.html
   ```
   with your port my port is 5143 for example in my case.


## Usage

Here are some key API endpoints:

- **Register a new user**:  
  `POST /api/account/register`

- **Login**:  
  `POST /api/account/login`

- **Get all books**:  
  `GET /api/book/GetAllBooks`

- **Get book by ID**:  
  `GET /api/book/GetBookById/{id}`

- **Add a new book**:  
  `POST /api/book/CreateBook`

For a comprehensive list of available endpoints, refer to the Swagger/OpenAPI documentation.


## Author

- **Einar Tómas Grétarsson**
- **Sigurður Valur Ásbjarnarson**

## Institution

Reykjavik University

## Course

Web Services (T-514-VEFT)
