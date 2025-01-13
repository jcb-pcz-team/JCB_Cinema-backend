# JCB_Cinema

## Installation

ASP.NET Core Runtime version 8 is required to run the application.
[Download .Net Core Runtime v8.0.**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Run the console and go to the project folder: "./JCB_Cinema-backend"

First you have to create a database. To do this, enter the command below :
```
dotnet ef database update --project JCB_Cinema.Infrastructure --startup-project JCB_Cinema.WebAPI
```

Now you can run the project by typing this command :
```
dotnet run --project JCB_Cinema.WebAPI
```

Now project is running.
