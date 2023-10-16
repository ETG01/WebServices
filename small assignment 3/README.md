# Ice Cream Management System

**Ice Cream Management System** is a comprehensive API designed to streamline the management of an ice cream inventory. With features that allow users to handle associated manufacturers, images, and categories, this system is a one-stop solution for all ice cream inventory needs.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Author](#author)
- [Institution](#institution)
- [Course](#course)

## Features

- **CRUD Operations for Ice Creams**: Seamlessly add, update, retrieve, and delete ice creams.
- **Image Management**: Easily associate images with specific ice creams.
- **Manufacturer Management**: Maintain detailed records of ice cream manufacturers.
- **Category Management**: Efficiently create, delete, and categorize ice creams. Also supports hierarchical category relationships.

## Technologies Used

- .NET Core 7.0
- Entity Framework Core
- SQLite

## Setup and Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ETG01/Datafication.git
   ```

2. **Navigate to Project Directory**:
   ```bash
   cd Datafication.WebAPI
   ```

3. **Install the Necessary NuGet Packages**:
   ```bash
   dotnet restore
   ```

4. **Setup the Database**: The project uses SQLite as its database. The connection string is configured in the appsettings.json file as follows:

    ```json
     "ConnectionStrings": {
    "IceCreamDb": "Data Source=../Datafication.Repositories/IceCreamDb.sqlite"
   }
    ```
   Ensure that the SQLite database file (IceCreamDb.sqlite) is present in the Datafication.Repositories directory.

5. **Database Migration**: Apply the latest migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```

6. **Populate the database**: Use the provided `population.sql` script to populate the database with sample data.


7. **Run the Application**:
   ```bash
   dotnet run
   ```
8. **Swagger**:
   #### The application will start and you can access it via a web browser. For example to access the Swagger UI, navigate to the following URL:
    ```sh
   http://localhost:5143/swagger/index.html
   ```
   with your port my port is 5143 for example.


## Usage

The system offers various API endpoints for different operations. Here are some key examples:

- **Get all ice creams**: `GET /api/icecreams`
- **Get ice cream by ID**: `GET /api/icecreams/{id}`
- **Add a new ice cream**: `POST /api/icecreams`
- ... [and so on]

For a comprehensive list of available endpoints, refer to the provided Swagger/OpenAPI documentation.


## Author

- **Einar Tómas Grétarsson**
- **Sigurður Valur Ásbjarnarson**

## Institution

Reykjavik University

## Course

Web Services (T-514-VEFT)
