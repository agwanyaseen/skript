using Cocona;
using Script;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommands<Commands>();

app.Run();