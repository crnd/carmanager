using System;

namespace CarManager.Persistence.Entities
{
	public interface IEntity
	{
		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }

		public DateTime? Deleted { get; set; }
	}
}
