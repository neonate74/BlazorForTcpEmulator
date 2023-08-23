using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TcpEmulatorForBlazor.Data;

namespace TcpEmulatorForBlazor
{
    public class Program
    {
        private static TcpEmulator.Connection.TcpConnector tcpConnector;

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
            tcpConnector.DataReceived += tcpConnector_DataReceived;
            tcpConnector.ConnectionConnected += tcpConnector_ConnectionConnected;
            tcpConnector.DoConnect();
        }

        private static void tcpConnector_ConnectionConnected(object sender, EventArgs e)
        {
            if (tcpConnector != null)
            {
                if (sender != null)
                {
                    //connectedString = "접속 성공";
                }
            }

            //Refresh();
        }

        private static void tcpConnector_DataReceived(object sender, EventArgs e)
        {
            if (tcpConnector != null)
            {
                //receivedString = tcpConnector.ReceivedDataString;
            }

            //Refresh();
        }
    }
}