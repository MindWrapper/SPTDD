using System.Data.Entity;

namespace Data
{
	public class TestDataDbContext : DbContext
	{
		public DbSet<TestRun> TestRuns { get; set; }
	}
}
