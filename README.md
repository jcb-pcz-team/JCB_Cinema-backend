# JCB_Cinema

## Installation

ASP.NET Core Runtime version 8 is required to run the application.
[Download .Net Core Runtime v8.0.**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Run the console and go to the project folder: "./JCB_Cinema-backend".
Now you have to configure some paremeters. Type this commands below.
```
dotnet user-secrets set "ConnectionStrings:JCB_CinemaDb" "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JCB_Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;" --project JCB_Cinema.WebAPI
```
```
dotnet user-secrets set "JWT:ValidAudience" "http://localhost:5193" --project JCB_Cinema.WebAPI
```
```
dotnet user-secrets set "JWT:ValidIssuer" "http://localhost:5193" --project JCB_Cinema.WebAPI
```
```
dotnet user-secrets set "JWT:Secret" "9fzkhnqU2VrJxZHRfFPDWpMFhRsOtGRA" --project JCB_Cinema.WebAPI
```
```
dotnet user-secrets set "JWTExtraSettings:TokenExpirySeconds" "4320000000" --project JCB_Cinema.WebAPI
```
```
dotnet user-secrets set "JWTExtraSettings:RefreshTokenExpiryMinutes" "3600" --project JCB_Cinema.WebAPI
```

Next you have to create a database. To do this, enter the command below :
```
dotnet ef database update --project JCB_Cinema.Infrastructure --startup-project JCB_Cinema.WebAPI
```

Now you can run the project by typing this command :
```
dotnet run --project JCB_Cinema.WebAPI
```

Now project is running.
To stop click ctr+c
