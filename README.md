# AllTheBeans

The repository contains my solution for a technical task set by Tombola titled all the beans (brief below). The solution can be found hosted at https://vaud.uk/allthebeans, or ran locally.

## Covering Note

### Running the solution

The solution can be ran manually or through Docker using the provided Dockerfile using the following instructions. In either case, the project will be located at http://localhost:8080/ (front-end) or http://localhost:5000/ (back-end)

If running manually

```bash
TODO instructions to build & run front & backend
```

If running via Docker

```bash
docker-compose up
```

Swagger is exposed even in production for ease of testing APIs. It can be found at http://localhost:5000/swagger

### Running tests

```bash
./runTests.sh
```

### Reasoning for technology choices

#### Front end

- **Vue.js** - I chose Vue as it was the framework listed in the Role Specification.
- **Typscript** - I have a strong preference for strict type systems. Also mentioned in the Role Spec.
- **Pinia** - Bundled with Vue, wanted to learn it if feasible, figured state management may come in handy for the task if scaled.
- **ESLint/Prettier** - Code formatting, quality control.
- **Docker** - Also mentioned in the Role Spec, and I wanted to host the solution on my server for ease of access, so a Docker build was handy anyway.
- **Vitest** - Comes bundled with Vue, seems a solid enough test platform, wanted to learn it.

#### Back end

- **DotNet Core** - I chose DotNet Core as it was mentioned in the brief, and I have been away from it for a few years so wanted a refresher.
- **SQLite** - I chose SQLite as I wanted to use EF and a SQL database, but figured a full SQL server was probably overkill for this task. Looking at the seed data, MongoDB would have been a solid choice, as the IDs used there are MongoDB ObjectIDs.
- **Hangfire** - I used Hangfire to schedule a daily cron job for choosing the bean of the day as it should be robust in deployment, functioning even if the application is slept due to no API requests.
- ***TODO*** Tests necessary here?

---

# To Do List

- Front End
    - ~~Responsive Layout~~
    - Components
        - List of beans (with search)
            - bean list item
        - Detailed bean view
        - Bean of the Day (interactable for more info)
    - Order form
- Back End
    - ~~SQL, Redis or mongoDB~~
    - ~~Load from JSON file (save to json file? - no need identified by brief)~~
    - ~~Fetch bean of the day~~
    - ~~Determine bean of the day (not the same as the previous day)~~
        - ~~Run task daily~~
    - ~~DB search query~~
    - ~~Clean up CRUD API~~
    - ~~Hide ID from API using DTO~~
    - ~~Ensure image string format is correct~~
    - ~~Check Docker works~~
- Check Integration works in Docker

---

# Task Brief

The challenge is to build a lightweight application to display the criteria in the test. We realise everyone has different levels of skill and experience when it comes to development, so we have listed two separate scenarios in this task for you to choose from. You can combine both if you wish or focus solely on one of the two scenarios provided.

## The Brief

You should use development best practices where appropriate, supporting principles we value including; security, performance, readability, testability, scalability, simplicity. You should also aim to achieve a clean separation of concerns between components in your solution. The scenarios are based on the business All The Beans who wants to market their products daily with a “bean of the day” mechanic.

### Scenario 1

1. Create a responsive layout using modern UI frameworks or libraries.
2. Using the attached JSON file implement components to display:
    - List of available coffee beans.
    - Detailed view of a selected coffee bean.
    - "Bean of the Day" feature which is interactable for more information.
3. Create an order form for customers to purchase coffee beans.
4. Implement a search feature to filter coffee beans by the available data.

### Scenario 2
1. Design and implement a relational database schema to store the bean details from the JSON file provided.
2. Create RESTful APIs to handle CRUD operations for coffee beans and Fetch the "Bean of the Day".
3. Implement business logic to ensure:
    - Each day, a new &quot;Bean of the Day&quot; is selected randomly from the available beans.
    - The selected bean cannot be the same as the previous day.
4. Implement a database search feature to show products available

## The Deliverable
- A bundled/archived repository showing your commit history or a link to an accessible private repository with your work in. We recommend Github.
- A covering note explaining the technology choices you have made.
- Any instructions required to run your solution and tests.

## Assessment Policy

We consider all candidates equally, fairly and without bias. To that end, we ask that you do not leave any personally identifying information in your submission (such as your name within an author field or file, or in use as test data).