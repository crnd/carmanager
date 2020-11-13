using System;

namespace CarManager.Persistence.Entities
{
	public interface IPublicEntity : IEntity
	{
		public Guid PublicId { get; set; }
	}
}
