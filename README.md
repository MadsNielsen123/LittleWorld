# LittleWorld

Simple full-stack starter with:
- TypeScript frontend (Vite)
- ASP.NET Core backend
- MariaDB via Docker Compose
- Dapper + DbUp for DB access and migrations

## Run MariaDB

```bash
docker compose up -d
```

## Run backend

```bash
cd backend
dotnet run
```

Backend URL: `http://localhost:5102`

## Run frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend URL: `http://localhost:5173`

On load, the frontend calls `/api/message` and displays the first value from the `AppMessages` table.
