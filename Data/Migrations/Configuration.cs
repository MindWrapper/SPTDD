namespace Data.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<Data.TestDataDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}
	}
}
