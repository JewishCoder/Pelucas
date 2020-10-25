using System.Collections.Generic;


namespace Pelucas.Common.ApiProvider
{
	public interface IApiParameters
	{
		IReadOnlyDictionary<string, string> GetParameters();
	}
}
