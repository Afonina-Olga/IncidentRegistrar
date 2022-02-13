using System;

namespace IncidentRegistrar.Tests.DataBuilders
{
	public abstract class DataBuilder<T> where T : class
	{
		public abstract T Build(int postfix = 1);

		protected void ThrowsArgumentNullExceptionIfStringIsNullOrEmpty(string input)
		{
			if (string.IsNullOrEmpty(input))
				throw new ArgumentNullException();
		}
	}
}
