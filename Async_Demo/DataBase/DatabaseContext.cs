using Async_Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Async_Demo.DataBase
{
    public class TestDb1Context : DbContext
    {
        public TestDb1Context(DbContextOptions<TestDb1Context> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }

    public class TestDb2Context : DbContext
    {
        public TestDb2Context(DbContextOptions<TestDb2Context> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }

    public class TestDb3Context : DbContext
    {
        public TestDb3Context(DbContextOptions<TestDb3Context> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }

    }
}