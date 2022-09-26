using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord_Spotify_Bot.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot
{
    public class Program
    {
        // Program entry point
        static Task Main(string[] args)
        {
            return new Program().MainAsync();
        }

        private DiscordSocketClient _client;

        private readonly CommandHandler _commandHandler;
        private readonly CommandService _commands;
        //todo: research
        //private readonly IServiceProvider _services;

        private readonly LoggingService _loggingService;
        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false
            });

            _loggingService = new LoggingService(_client, _commands);

            _commandHandler = new CommandHandler(_client, _commands);
            // Setup your DI container.
            //_services = ConfigureServices();
        }

        private async Task MainAsync()
        {
            await _commandHandler.InstallCommandsAsync();

            var token = File.ReadAllText(@".\discordToken.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Run(EndMain);
        }

        private Task EndMain()
        {
            string hostInput;
            do
            {
                hostInput = Console.ReadLine().ToLower();
            } 
            while ((String.Equals(hostInput, "stop") == false));

            Console.WriteLine("Ending Main...");
            _client.StopAsync();
            _client.Dispose();
            if (Directory.Exists(@".\songs"))
            {
                Directory.Delete(@".\songs", true);
            }
            return Task.CompletedTask;
        }
    }
}
