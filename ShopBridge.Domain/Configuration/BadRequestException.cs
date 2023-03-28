using System;

namespace ShopBridge.Domain.Configuration
{
    public class BadRequestException<T> : ApplicationException
	{
		public BadRequestException(string message) : base(message)
		{
		}
	}
}
