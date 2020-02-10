using Microsoft.EntityFrameworkCore;

namespace ArtContainer.Data.ObjectContext
{
    public class ArtObjectContext : DbContext
    {
        public ArtObjectContext(DbContextOptions<ArtObjectContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
