using Infrastructure.Configurations;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MUD.Worlds;
using System.Net;
using System.Runtime.CompilerServices;

namespace MUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices( services =>
                {
                    services.AddSingleton<JsonFileRepository<Player>>();
                    services.AddSingleton<FileSources>();
                    services.AddSingleton<ScoreSystem>();
                    services.AddSingleton<GameManager>();
                })
                .Build();
            Console.Title = "MUD - The Magical World";
            GameManager gameManager = host.Services.GetRequiredService<GameManager>();
            gameManager.StartMenu();
        }
    }
}