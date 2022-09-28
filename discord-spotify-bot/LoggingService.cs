using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot
{
    public class LoggingService
    {
		public LoggingService(DiscordSocketClient client, CommandService command)
		{
			client.Log += LogAsync;
			command.Log += LogAsync;
		}
		private Task LogAsync(LogMessage message)
		{
			if (message.Exception is CommandException cmdException)
			{
				var today = DateTime.Now.ToString("M-d-yyyy");
				Log.Logger = new LoggerConfiguration()
							.WriteTo.Console()
							.WriteTo.File(@"C:\Users\xseam\Desktop\" + today + ".txt", rollingInterval: RollingInterval.Day)
							.CreateLogger();
				Log.Error($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
					+ $" failed to execute in {cmdException.Context.Channel}.");
				Log.Error(cmdException.ToString());
				Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
					+ $" failed to execute in {cmdException.Context.Channel}.");
				Console.WriteLine(cmdException);
			}
			else
				Console.WriteLine($"[General/{message.Severity}] {message}");

			return Task.CompletedTask;
		}
	}
}
