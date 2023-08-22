using Microsoft.EntityFrameworkCore;

namespace LearningCenter.WhatIsMinimalApi.Repository
{
    public class LiftDb : DbContext
    {
        public LiftDb(DbContextOptions<LiftDb> options) : base(options) { }

        public DbSet<Entity.Lift> Lifts => Set<Entity.Lift>();
    }
}
