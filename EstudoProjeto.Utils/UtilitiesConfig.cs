using Microsoft.Extensions.Configuration;

namespace ProjetoEstudo.Utils
{
	public class UtilitiesConfig
	{
		public const string DEFAULT_CONFIG_FILE = "appsettings.json";
		private const string KEY_EXTERNAL_CONFIG_FILES = "arquivo.externo";
		private const string SEPARATOR_EXTERNAL_CONFIG_FILES = ",";

		public static IConfiguration AppSettings { get; set; }

		static UtilitiesConfig()
		{

			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder = AddJsonFileToConfigurationBuilder(configurationBuilder, DEFAULT_CONFIG_FILE, false);

			IConfiguration mainConfig = configurationBuilder.Build();

			string externalConfigFilesStr = mainConfig[KEY_EXTERNAL_CONFIG_FILES];

			if (!string.IsNullOrEmpty(externalConfigFilesStr))
			{
				string[] externalConfigFiles = externalConfigFilesStr.Split(SEPARATOR_EXTERNAL_CONFIG_FILES);
				foreach (string configFile in externalConfigFiles)
				{
					configurationBuilder = AddJsonFileToConfigurationBuilder(configurationBuilder, configFile, true);
				}//foreach
			}//if

			configurationBuilder = configurationBuilder.AddEnvironmentVariables();

			IConfiguration config = configurationBuilder.Build();

			UtilitiesConfig.AppSettings = config;
		}// func

		private static IConfigurationBuilder AddJsonFileToConfigurationBuilder(IConfigurationBuilder configurationBuilder, string configFilePath, bool isOptional)
		{
			configurationBuilder = configurationBuilder.AddJsonFile(configFilePath, isOptional, true);
			return configurationBuilder;
		}//func

		public static string GetAppSetting(string key)
		{
			string valueRaw = UtilitiesConfig.AppSettings[key];
			return valueRaw;
		}// func

	}
}
