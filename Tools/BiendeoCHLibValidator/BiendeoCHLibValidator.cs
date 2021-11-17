using BepInEx.Logging;
using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLibValidator {
	class BiendeoCHLibValidator {
		internal static bool LoggingEnabled = true;

		static void Main(string[] args) {
			var logger = new ManualLogSource("stdout");
			logger.LogEvent += LogEvent;
			LoggingEnabled = false;

			// The current working directory should be the repository root.
			WrapperBase.InitializeWrappers(logger);

			LoggingEnabled = true;

			var validators = new IValidator[] {
				new StandaloneVsLauncherValidator(logger),
				new SourceMapValidator(logger),
				new WrapperValidator(logger),
			};

			foreach (var validator in validators) {
				if (!validator.AssertWorkingDirectory()) {
					Environment.Exit(1);
				}
			}
			foreach (var validator in validators) {
				validator.Validate();
			}
		}

		private static void LogEvent(object sender, LogEventArgs e) {
			if (!LoggingEnabled) return;
			ConsoleColor oldBackground = Console.BackgroundColor;
			ConsoleColor oldForeground = Console.ForegroundColor;

			switch (e.Level) {
				case LogLevel.Debug:
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
				case LogLevel.Info:
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case LogLevel.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case LogLevel.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case LogLevel.Fatal:
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
				default:
					Console.ForegroundColor = ConsoleColor.White;
					break;
			}

			Console.WriteLine($"[{e.Level,-7}] {e.Data}");

			Console.BackgroundColor = oldBackground;
			Console.ForegroundColor = oldForeground;
		}
	}
}
