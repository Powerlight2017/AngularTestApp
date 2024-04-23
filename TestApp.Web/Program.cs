using TestApp.Services;

var builder = WebApplication.CreateBuilder(args);

StartupCore.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
StartupCore.Configure(app);
StartupCore.InitializeDB(app.Services);

app.Run();