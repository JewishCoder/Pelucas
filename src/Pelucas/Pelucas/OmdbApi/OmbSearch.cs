using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Pelucas.OmdbApi
{
	[DataContract]
	public class OmbSearch
	{
		[JsonProperty("Search")]
		public OmdbMovie[] Movies { get; set; }

		[JsonProperty("totalResults")]
		public int TotalResults { get; set; }

		[JsonProperty("Response")]
		public bool Response { get; set; }
	}
}
