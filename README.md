# 📚 Bookstore (Console Application)

My first project using **Entity Framework Core**, which connects to a database and allows data management from a console interface. A CRUD-style application designed to manage the resources of a bookstore.

Main project features:

- **N-Tier Architecture:** Logical separation of business logic (services), data access layer (repositories and EF Core), and presentation (console UI).
- **Dependency Injection (DI):** Usage of an IoC container (**Ninject**) to manage the object lifecycle.
- **Full CRUD operations:** Create, read, update, and delete books, authors, and publishers.
- **Database:** Persistent data storage using a relational database (SQL Server).
- **Secure configuration:** Connection String management via the `appsettings.json` file.

## 🛠 Tech Stack

### Application & Logic

- **.NET 8** - Modern platform for building applications.
- **C#** - Main programming language.
- **Entity Framework Core** - ORM for database operations.
- **LINQ** - Query language for data manipulation.

### User Interface

- **Console application** - Text-based, command-line user interface.

### Architecture & Patterns

- **N-Tier Architecture** - Logical separation of the Data Access layer from business logic and presentation.
- **Dependency Injection** - Implemented using the **Ninject** library.
- **Repository Pattern** - Abstraction of the data layer that acts as a mediator between business logic and the database.
- **Service Pattern** - Extraction of the application's business logic into separate classes.
- **SOLID Principles** - Code organization with an emphasis on the Single Responsibility Principle.

## 🚀 Running the Project

Requirements: **.NET 8 SDK** and an installed **SQL Server** database server (e.g., the free Express edition).

1.  **Clone the repository:**

    ```bash
    git clone [https://github.com/X3raFin/EntityFramework-project-with-console-UI.git](https://github.com/X3raFin/EntityFramework-project-with-console-UI.git)
    cd EntityFramework-project-with-console-UI
    ```

2.  **Configure the Connection String (`appsettings.json`):**

    In the main project folder, there is an `appsettings.json` file (or create one if it is missing). Make sure it contains a valid Connection String pointing to your local server.

    _Note: Avoid Polish characters in the database name (e.g., use `KsiegarniaDB` instead of `Księgarnia`) to prevent SQL Server login errors!_

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=KsiegarniaDB;Trusted_Connection=True;TrustServerCertificate=True;"
      }
    }
    ```

3.  **Apply database migrations:**

    Ensure you have the EF Core tools installed (`dotnet tool install --global dotnet-ef`), and then run the following command in the terminal to create the database and tables:

    ```bash
    dotnet ef database update
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```

## 📬 Contact

Created by **Kacper Jankowski**.

- 🌐 **LinkedIn:** [LinkedIn](https://www.linkedin.com/in/kacper-jankowski-webdev/)
- 📧 **Email:** kacper.jankowski.webdev@gmail.com
- 💼 **Portfolio:** [Portfolio](https://portfolio-neon-one-lb87d8f29l.vercel.app/)
