using System.IO;
using Newtonsoft.Json;

namespace Pelucas.Common.ApiProvider
{
	public static class ApiProviderFactory
	{
		#region Helpers

		class ApiModel 
		{
			public string Name { get; set; }

			public string EndPoint { get; set; }

			public ApiKeyModel Key { get; set; }
		}

		class ApiKeyModel 
		{
			public string Name { get; set; }

			public string Value { get; set; }
		}

		#endregion

		public static ApiProvider<T> Create<T>(string configPath) 
			where T : class, IApiParameters, new()
		{
			if(!File.Exists(configPath)) 
			{
				throw new FileNotFoundException(configPath);
			}

			using var fileStream = File.OpenRead(configPath);
			using var streamReader = new StreamReader(fileStream);
			using var jsonReader = new JsonTextReader(streamReader);
			var result = JsonSerializer.CreateDefault().Deserialize<ApiModel>(jsonReader);

			return new ApiProvider<T>(result.Name, result.EndPoint, new ApiKey(result.Key.Name, result.Key.Value))
			{
				Parameters = new T(),
			};
		}
	}
}
