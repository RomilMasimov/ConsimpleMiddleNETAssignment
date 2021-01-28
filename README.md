# Consimple Middle .NET Assignment

Before launch, create migration and update database:

```sh
dotnet ef migrations add initial
dotnet ef database update
```

Then launch the app:

```sh
dotnet run
```

Navigate to https://localhost:5001, and explore the API via Swagger UI.