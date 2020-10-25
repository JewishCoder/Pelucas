using System.IO;

namespace Pelucas.Configuration
{
	public class ConfigurationManager
	{
		private string RootPath { get; }

		public ConfigurationManager()
		{
			RootPath = Path.Combine(Directory.GetCurrentDirectory(), "Configuration");
		}

		public string GetApiConfigPath() 
		{
			return Path.Combine(RootPath, "ApiConfiguration.json");
		}
	}
}
