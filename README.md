# BobsCorn

This website was built to sell corn from Bob's farm, allowing 1 ear of corn per client per minute (non-cumulative).

This project is a .NET Web API with two controllers that provide data to a frontend built with HTML, CSS, and JavaScript using the Fetch API.

## Features

- RESTful API with 2 controllers
- JSON-based responses
- Frontend integration using the Fetch API
- Simple HTML/CSS interface

## Tech Stack

- .NET 10 Web API
- C#
- HTML5 / CSS3
- JavaScript (Fetch API)

## Project Structure

- /Controllers
  - ClientCornController.cs
  - CornController.cs
- /wwwroot
  - index.html
  - styles.css
  - script.js
- Program.cs

## Static Pages

The frontend is built with vanilla JavaScript, simple HTML/CSS, and uses Fetch to call API endpoints.

## API Endpoints

- **GET /api/ClientCorn**
  Returns the total amount of corn purchased by a client.

- **POST /api/Corn**
  Checks if there is available corn to buy. If so, it decreases the stock by 1 and assigns it to the client. Otherwise, it returns an error message.

## Clean Code

The Repository pattern is applied to separate concerns and improve maintainability. The repository is injected into the controllers to handle data access, enabling easier testing and future scalability.

## Error Handling

This is a simple implementation, but it includes basic error handling to return appropriate HTTP status codes and messages. Errors are also logged to the console for debugging purposes.

## Throttling

To enforce the limit of 1 ear of corn per client per minute, a throttling mechanism is implemented in the `POST /api/Corn` endpoint and passing the client name in the header.

The system tracks the last purchase time for each client and checks it before allowing a new purchase. If a client attempts to buy corn within the cooldown period, the API returns status code **429 (Too Many Requests)**.

## Future Improvements

- Add authentication
- Improve UI design
- Add logging storage providers
- Add unit tests for API endpoints