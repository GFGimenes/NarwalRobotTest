using Microsoft.EntityFrameworkCore;
using NarwalRobotTest.Models;

namespace NarwalRobotTest.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Robot> Robot { get; set; }

        //public DbSet<Arm> Arms { get; set; }

        //public DbSet<Head> Head { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
