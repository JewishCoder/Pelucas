using Pelucas.Common.ApiProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pelucas.OmdbApi
{
	public class OmdbApiParameters : IApiParameters
	{
		private const string SearchParameterName = "s";

		private Dictionary<string, string> Parameters { get; }

		public string SearchQuery { get; set; }

		public OmdbApiParameters()
		{
			Parameters = new Dictionary<string, string>();
		}

		public IReadOnlyDictionary<string, string> GetParameters()
		{
			Parameters.Clear();
			if(SearchQuery != null)
			{
				Parameters.Add(SearchParameterName, SearchQuery);
			}

			return Parameters;
		}
	}
}
