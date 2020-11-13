using System;

namespace CarManager.Persistence.Entities
{
	public class Car : IPublicEntity
	{
		public int Id { get; set; }

		public Guid PublicId { get; set; }

		public string Make { get; set; }

		public string Model { get; set; }

		public int Power { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }

		public DateTime? Deleted { get; set; }
	}
}
