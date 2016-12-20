This project is an example from this [post](http://drugalya.com/stored-procedures-guided-by-tests). It shows how to grow the stored procedure, guided by tests, by solving the task defined below:

*Your organization runs tests on a build farm. For each test, you want to calculate the Success Rate. If “Test1” passes 3 out of 4 times, the Success Rate is 75%. For performance reasons, you decide to use a stored procedure. You want the stored procedure to be testable.*

The project uses Entity Framework code-first. There are 2 C# projects.

**Data.csproj** contains the single domain class TestRun, DBContext, and everything related to the database.

DBContext creates the stored procedure when the application starts. TestDataDbConectInitializer::initializeDababase in
[DBConetxt.cs](https://github.com/MindWrapper/SPTDD/blob/master/Data/DBContext.cs)

Note: This approach works well when there is a single application instance. However, depending on the situation, you might want to use more proper methods to create it: migrations, for example.


**Data.Tests.csproj** contains code for tests. Here I use DBContext context to setup the test database and erase data in all tables before each test.

```cs
[SetUp]
public void Setup()
{
    m_Context = new TestDataDbContext("TestConnection");
    m_StatsService = new TestRunStats(m_Context);
    CleanupDatabase();
}
```
See [TestRunStatsTests.cs](https://github.com/MindWrapper/SPTDD/blob/master/Data.Tests/TestRunStatsTests.cs)

**Try it yourself**

I intentionally left one failed test and disabled to allow you to evolve it one step forward. Before you see it fail, check if your environment matches the following requirements:

- Visual Studio 2015 or later
- ReSharper or other NUnit test runner
- SQL Server Express edition (at least)

Steps

1. Clone latest revision
2. Try to run the tests and check if DB and stored procedure exist: </br> ![Alt text](http://i.imgur.com/0sWWo1p.png)
3. Enable ‘GetRunStats_OneTestRunInDatabase_ReturnsStatsEntryWithProperName’ and try to make it green

Will be happy to hear any feedback and answer questions. Just write me an email ydrugalya (on gmail.com), submit an issue to the project or create a pull request.
