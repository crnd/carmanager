using Bogus;
using CarManager.Persistence.Entities;

namespace CarManager.Tests.Shared.Generators
{
	public abstract class GenericGenerator<TEntity, TCreateCommand, TUpdateCommand, TDeleteCommand> : IEntityGenerator<TEntity>
		where TEntity : class, IEntity
		where TCreateCommand : class
		where TUpdateCommand : class
		where TDeleteCommand : class
	{
		protected Faker<TEntity> entity;
		protected Faker<TCreateCommand> createCommand;
		protected Faker<TUpdateCommand> updateCommand;
		protected Faker<TDeleteCommand> deleteCommand;

		public TEntity Entity => entity.Generate();

		public TCreateCommand CreateCommand => createCommand.Generate();

		public TUpdateCommand UpdateCommand => updateCommand.Generate();

		public TDeleteCommand DeleteCommand => deleteCommand.Generate();
	}
}
