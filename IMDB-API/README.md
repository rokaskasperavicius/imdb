## Running the Project
Create appsettings.Development.json with the following data:

```
{
  "JwtSecretKey": "the-secret-must-be-at-least-256-bit-long",
  "ConnectionStrings": {
    "ImdbDbContext": "Host=;Database=;Username=;Password="
  }
}
```