# Online Learning Platform (Backend) — ASP.NET Core Web API

An Online Learning Platform backend built with **ASP.NET Core Web API** using a **layered architecture (DAL / BLL / API)**.
It supports course management, quizzes, enrollments, progress tracking, certificates, and reviews.

## Tech Stack
- C#, ASP.NET Core Web API
- SQL Server
- EF Core
- JWT Authentication & Authorization
- AutoMapper 

## Architecture
- **OnlineLearning.DataAccessLayer**: Entities, repositories/data access
- **OnlineLearning.BussinessLayer**: Services, business rules, validation
- **OnlineLearningPlatform.Presentation**: API Controllers, DTOs, middleware

## Core Features
- Authentication (Register/Login) + JWT
- Users & Roles (Student / Instructor / Admin)
- Courses, Lessons
- Quizzes, Questions, Answers
- Enrollments + Progress tracking
- Certificates
- Reviews & Ratings

## Database
SQL Server database with relational tables and constraints.
<img width="1365" height="783" alt="Screenshot 2025-12-07 082314" src="https://github.com/user-attachments/assets/579849b3-031a-4472-967f-cad6d22afec3" />

