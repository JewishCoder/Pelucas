using System;
using System.Text;

namespace Pelucas.Common.ApiProvider
{
	public class ApiKey
	{
		private string _value;

		public string Name { get; }

		public ApiKey(string name, string value)
		{
			Name = name;
			_value = value;
		}

		public void SetKey(StringBuilder builder) 
		{
			if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(_value)) 
			{
				throw new InvalidOperationException("Can't set api key. No name or key specified");
			}

			builder.AppendFormat("{0}={1}", Name, _value);
		}
	}
}
