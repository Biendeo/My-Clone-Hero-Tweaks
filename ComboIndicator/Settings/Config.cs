using Common.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace ComboIndicator.Settings {
	[Serializable]
	public class Config {
		public int Version;
		public string TweakVersion;
		public bool SilenceUpdates;

		[XmlIgnore]
		public bool SeenChangelog;

		public Config() {
			Version = 1;
			TweakVersion = "0.0.0";
			SilenceUpdates = false;
		}

		public static Config LoadConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ComboIndicatorConfig.xml"));
			if (configFilePath.Exists) {
				var configString = File.ReadAllText(configFilePath.FullName);
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
					return serializer.Deserialize(configIn) as Config;
				}
			} else {
				var c = new Config();
				c.SaveConfig();
				return c;
			}
		}

		public void SaveConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ComboIndicatorConfig.xml"));
			if (configFilePath.Exists) configFilePath.Delete();
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.OpenWrite()) {
				serializer.Serialize(configOut, this);
			}
		}
	}
}
