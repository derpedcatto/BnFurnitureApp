# BnFurniture – E-Commerce Furniture Platform

This is an ITStep Diploma Project showcasing a full-featured e-commerce platform for furniture sales with comprehensive administration capabilities, built with modern web technologies and cloud infrastructure. [See live presentation here (starting from 25:10)](https://drive.google.com/file/d/1vLotWC50-OHavhYtRW6AX-XQKAsF6-ug/view?usp=drive_link)
<img width="2209" height="1075" alt="vivaldi_k4lez75zWV" src="https://github.com/user-attachments/assets/c86049dd-9f0b-4a7e-bb52-9b50944b8fd5" />
<img width="2205" height="1076" alt="vivaldi_4jLx6sw09Z" src="https://github.com/user-attachments/assets/f7cdb8a9-c0be-4015-a278-05f44ca02d1e" />
<img width="2213" height="1075" alt="vivaldi_n0lBhLwmwk" src="https://github.com/user-attachments/assets/a23f8225-ef25-41fc-a3d6-93e326b5c17f" />

## Project Information

**ITStep Diploma Project – Team Members:**
- **Oleksandr Vannovskyi** – Team Lead, Full-stack Developer, Documentation, Dashboard
- **Oleksii Gorin** – Back-end Developer
- **Oleksandr Gurnik** – Front-end Developer

**Diploma Materials**
- [Project Presentation Slideshow](https://docs.google.com/presentation/d/1A6jJgtKOd_kN29p8xEgQB9y1iHCAUAGV/edit?usp=drive_link&ouid=114272261084941485988&rtpof=true&sd=true)
- [Project Video Presentation](https://drive.google.com/file/d/1vLotWC50-OHavhYtRW6AX-XQKAsF6-ug/view?usp=drive_link)

## Demo Notice

This application cannot be demonstrated locally as it requires:
- Azure Blob Storage for media file management
- Azure SQL Database for data persistence
- Configured cloud infrastructure and connection strings

To run this project, you must have access to Azure resources or set up your own cloud infrastructure.

## Architecture Overview

### Dashboard
- [Project Dashboard Archive (Notion)](https://derpedcatto.notion.site/BN-Archived-7c18a7ffcd394260887484f867998b85)

### Frontend (Customer Portal)
Modern responsive web application for browsing and purchasing furniture

### Backend (API)
RESTful API handling business logic, authentication, and data management
- [Database Schema Overview](https://drive.google.com/file/d/18pzEe2dgFcoMstyx1c48AzRamwSQ6KJO/view?usp=sharing)
- [Database Schema Detailed Description & Changelog](https://derpedcatto.notion.site/Database-d60b8c2a033147b1bb53e583fa64e041)

## Technologies Used

### Core Technologies
- **ASP.NET Core** – Backend framework
- **Entity Framework Core** – ORM for database operations
- **React** – Frontend UI library
- **TypeScript** – Type-safe JavaScript
- **C#** – Primary backend language

### Cloud & Infrastructure
- **Azure SQL Database** – Relational database
- **Azure Blob Storage** – Media file storage
- **Azure App Services** – Application hosting (if applicable)
- **Aiven** - MySQL Database hosting

### Frontend Technologies
- **React Router** – Client-side routing
- **Axios / Fetch API** – HTTP client
- **CSS Modules** – Component styling
- **React Redux** – State management

### Backend Technologies
- **ASP.NET Core Web API** – RESTful services
- **JWT Authentication** – Secure user authentication
- **FluentValidation** – Input validation

### Development Tools
- **Git** – Version control
- **Visual Studio / VS Code** – IDEs
- **Postman** – API testing
- **npm** – Package management

## Features

### Customer Features
- Product catalog with categories and filtering
- Product search and sorting
- Shopping cart functionality
- User authentication and profiles
- Responsive design for mobile, tablets and desktops

### Admin Dashboard Features (Server only)
- Product management (CRUD operations)
- Category management
- Order management and status updates
- User management
- Media upload and management

## Getting Started

### Prerequisites
- .NET 6.0 SDK or higher
- Node.js (v16+) and npm
- Azure account with:
  - Azure SQL Database instance
  - Azure Blob Storage account
- SQL Server Management Studio (optional)

### Backend Setup

1. **Clone the repository:**
```bash
git clone https://github.com/derpedcatto/BnFurnitureApp.git
cd BnFurnitureApp
```

2. **Configure Azure Services:**
- Create an Azure SQL Database
- Create an Azure Blob Storage account
- Note down connection strings

3. **Update Configuration:**
Create or update `appsettings.json` in the backend project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_AZURE_SQL_CONNECTION_STRING"
  },
  "AzureStorage": {
    "ConnectionString": "YOUR_BLOB_STORAGE_CONNECTION_STRING",
    "ContainerName": "furniture-images"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "BnFurnitureApp",
    "Audience": "BnFurnitureApp"
  }
}
```

4. **Apply Database Migrations:**
```bash
dotnet ef database update
```

5. **Run the Backend:**
```bash
dotnet run
```

### Frontend Setup

1. **Navigate to Frontend directory:**
```bash
cd Frontend  # or your frontend project folder
```

2. **Install dependencies:**
```bash
npm install
```

3. **Configure API endpoint:**
Update the API base URL in your configuration file (e.g., `src/config.ts` or `.env`):
```
REACT_APP_API_URL=https://localhost:5001/api
```

4. **Run the Frontend:**
```bash
npm start
```

## Environment Variables

Create appropriate configuration files for sensitive data:

**Backend (`appsettings.Development.json`):**
- Database connection string
- Azure Blob Storage connection
- JWT secret key
- CORS origins

**Frontend (`.env.local`):**
- API base URL
