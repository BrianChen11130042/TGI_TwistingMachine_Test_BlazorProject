using CommonRazorLibraryB.Library.TGITwistingMachine;
using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Library.TGITwistingMachine.Property;
using CommonRazorLibraryB.Manager.ModbusTcp.Master;
using CommonRazorLibraryB.Tools.NLogWritter;
using CommonRazorLibraryB.UIComponentTools.ToastMessage;
using Microsoft.Extensions.Hosting.WindowsServices;
using TGI_TwistingMachine_Test_BlazorProject.Components;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer;
using TGI_TwistingMachine_Test_BlazorProject.Scope;
using TGI_TwistingMachine_Test_BlazorProject.Service.MachineService;

//var builder = WebApplication.CreateBuilder(args);

//指定在Windows Service」環境下正確運作
var webApOpts = new WebApplicationOptions
{
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ?
        AppContext.BaseDirectory : default,
    Args = args
};
var builder = WebApplication.CreateBuilder(webApOpts);
builder.Host.UseWindowsService();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options =>
{
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddMvc();

string filePath = System.AppDomain.CurrentDomain.BaseDirectory;

builder.Services.AddScoped<IToastMessage, ToastMessage>();

builder.Services.AddSingleton<NLogWritter>(provider => new NLogWritter(filePath));

builder.Services.AddSingleton<ObserverLibrary>();

builder.Services.AddSingleton<ModbusTcpMasterManager>(provider => new ModbusTcpMasterManager(filePath));

builder.Services.AddSingleton<TgiTwistConfigManager<ETwist>>(provider => new TgiTwistConfigManager<ETwist>(filePath));
builder.Services.AddSingleton<TgiTwistPropertyManager<ETwist>>(provider => new TgiTwistPropertyManager<ETwist>(filePath));
builder.Services.AddSingleton<TgiTwistLibrary<ETwist>>();

builder.Services.AddSingleton<TgiScope>();

builder.Services.AddSingleton<IMachineService, MachineService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();
