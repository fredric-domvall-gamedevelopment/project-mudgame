using Infrastructure.Configurations;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MUD.Worlds;

namespace MUD
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<JsonFileRepository<Player>>();
                    services.AddSingleton<FileSources>();
                    services.AddSingleton<ScoreSystem>();
                    services.AddSingleton<PlayerSystem>();
                    services.AddSingleton<RewardSystem>();

                    services.AddSingleton<PlayerManager>();
                    services.AddSingleton<PlayerCreator>();
                    services.AddSingleton<GameManager>();
                    services.AddSingleton<GameWorld>();
                    services.AddSingleton<Sea>();
                    services.AddSingleton<Mountains>();
                })
                .Build();

            Console.Title = "MUD - The Magical World";

            GameManager gameManager = host.Services.GetRequiredService<GameManager>();
            await gameManager.StartMenu();
        }
    }
}