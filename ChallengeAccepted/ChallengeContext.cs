using ChallengeAccepted.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAccepted
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext() { }

        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; } = null!;
    }
}
