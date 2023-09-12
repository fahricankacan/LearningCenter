using LearningCenter.Entity.Concrate;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Repository
{
    public class LiftDb : DbContext
    {
        public LiftDb(DbContextOptions<LiftDb> options) : base(options) { }

        public DbSet<Lift> Lifts => Set<Lift>();
    }
}
