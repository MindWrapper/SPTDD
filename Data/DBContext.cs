using System.Data.Entity;
using Data.Migrations;

namespace Data
{
	public class TestDataDbContext : DbContext
	{
		public TestDataDbContext()
			: this("TestConection")
		{
		}
		public TestDataDbContext(string nameOrConnectionString) 
			: base(nameOrConnectionString)
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<TestDataDbContext>());
			Database.Initialize(true);

			Database.SetInitializer(new TestDataDbConectInitializer());
			Database.Initialize(true);
		}
		public DbSet<TestRun> TestRuns { get; set; }
	}

	internal class TestDataDbConectInitializer : MigrateDatabaseToLatestVersion<TestDataDbContext, Configuration>
	{
		public override void InitializeDatabase(TestDataDbContext context)
		{
			context.Database.ExecuteSqlCommand(Resources.DropGetRunStats);
			context.Database.ExecuteSqlCommand(Resources.CreateGetRunStats);
		}
	}
}
