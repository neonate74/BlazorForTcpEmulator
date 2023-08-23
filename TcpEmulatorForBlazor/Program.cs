using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TcpEmulatorForBlazor.Data;

namespace TcpEmulatorForBlazor
{
    public static class Program
    {
        private static TcpEmulator.Connection.TcpConnector tcpConnector;
        public static TcpEmulator.Connection.TcpConnector TcpConnector => tcpConnector;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            OnInitializedTcpConnector();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        private static void OnInitializedTcpConnector()
        {
            tcpConnector = new TcpEmulator.Connection.TcpConnector();
        }
    }
}