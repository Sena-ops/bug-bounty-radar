# Bug Bounty Radar

This project provides an API built with **.NET 8** and a front‑end built with **React + TypeScript**. It imports bug bounty programs from the HackerOne API and exposes CRUD endpoints.

## Running the API

```bash
cd src/api
dotnet run
```

The API uses PostgreSQL; connection information is configured in *appsettings.json*.

## Running the Web Front-End

```bash
cd src/web
npm install
npm run dev
```

## Tests

Back-end tests run with `dotnet test` and front-end tests with `npm run test`.

