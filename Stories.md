# User Stories & Technical Requirements – Friberg Quote Factory API

## API Functionality

1. **As a user, I want to retrieve a random quote, so that I can get inspiration.**

2. **As a user, I want to retrieve a random quote from a specific category (entrepreneurship, self-development, motivation, leadership, success), so that I can get tailored inspiration.**

3. **As a user, I want to submit a new quote with a quote text and category, so that I can contribute to the platform.**

4. **As an administrator, I want to list all quotes that have not yet been approved, so that I can review new submissions.**

5. **As an administrator, I want to approve a quote by its ID, so that it becomes available for users via the API.**

---

## Testing & Quality Assurance

6. **As a developer, I want to use Test-Driven Development (TDD) and write unit tests for the repository class, so that I can verify data logic with seeded data.**

7. **As a developer, I want to create integration tests using WebApplicationFactory, so that I can ensure the API responds correctly to HTTP requests.**

8. **As a developer, I want to use xUnit, [Fact], and Assert.Contains in my tests, so that I can validate API responses.**

---

## Technical & Data Model Requirements

9. **As a developer, I want the API to be built in ASP.NET Core and use the Repository Pattern for data access, so that the architecture is modern and maintainable.**

10. **As a developer, I want each quote to have the following properties:**
    - **Id (GUID)**
    - **Quote (string)**
    - **Category (string; must be one of the predefined categories)**
    - **Approved (bool)**

11. **As a developer, I want the API to be well-documented and easy to use, so that the app development team can consume it (Swagger-generated documentation is sufficient).**

---

## Categories

- entrepreneurship
- self-development
- motivation
- leadership
- success

