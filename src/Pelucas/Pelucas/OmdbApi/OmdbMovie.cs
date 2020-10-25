using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Pelucas.OmdbApi
{
	[DataContract]
	public class OmdbMovie
	{
		[JsonProperty("imdbID")]
		public string Id { get; set; }

		[JsonProperty("Title")]
		public string Title { get; set; }

		[JsonProperty("Year")]
		public string Year { get; set; }

		[JsonProperty("Type")]
		public string Type { get; set; }

		[JsonProperty("Poster")]
		public string Poster { get; set; }
	}
}
