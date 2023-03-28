using System;

namespace ShopBridge.Domain.Configuration
{
    public class EntityNotFoundException<T> : ApplicationException
	{
		public EntityNotFoundException(string message) : base(message)
		{
		}
	}
}
