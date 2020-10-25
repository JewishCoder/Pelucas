using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;

namespace Pelucas.Common.ApiProvider
{
	public class ApiProvider<T> : IDisposable
		where T: class, IApiParameters
	{
		private const string ParameterSeparator = "&";
		private const string ParameterFlag = "?";

		private HttpClient HttpClient { get; }

		private ApiKey ApiKey { get; }

		public string Name { get; }

		public string ApiEndPoint { get; }

		public T Parameters { get; set; }

		public bool IsDisposed { get; private set; }

		public ApiProvider(string name, string apiEndPoint, ApiKey apiKey)
		{
			HttpClient = new HttpClient();

			ApiKey = apiKey;
			Name = name;
			ApiEndPoint = apiEndPoint;
		}

		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{
				HttpClient.Dispose();
			}

			Parameters = null;
		}

		public void Dispose()
		{
			if(IsDisposed) return;

			Dispose(true);
			GC.SuppressFinalize(this);
			IsDisposed = true;
		}

		public async Task<TData> ReceiveJsonDataAsync<TData>() 
		{
			using var message = await HttpClient.GetAsync(BuildRequest())
					.ConfigureAwait(continueOnCapturedContext: false);
			using var stream = await message.Content.ReadAsStreamAsync()
				.ConfigureAwait(continueOnCapturedContext: false);
			using var streamReader = new StreamReader(stream);
			using var reader = new JsonTextReader(streamReader);

			return JsonSerializer.CreateDefault().Deserialize<TData>(reader);
		}

		public Task<Stream> ReadAsStreamAsync(string request = null) 
		{
			return HttpClient.GetStreamAsync(request ?? BuildRequest());
		}

		public string BuildRequest() 
		{
			var builder = new StringBuilder();
			builder.Append(ApiEndPoint);
			builder.Append(ParameterFlag);
			ApiKey.SetKey(builder);
			if(Parameters != null) 
			{
				SetParameters(builder);
			}

			return builder.ToString();
		}

		private void SetParameters(StringBuilder builder) 
		{
			var parameters = Parameters.GetParameters();
			if(parameters == null || parameters.Count == 0) return;

			builder.Append(ParameterSeparator);
			var index = 0;
			foreach(var parameter in parameters)
			{
				index++;
				builder.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
				if(index < parameters.Count)
				{
					builder.Append(ParameterSeparator);
				}
			}
		}
	}
}
