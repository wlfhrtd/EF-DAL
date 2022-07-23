using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Dal.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
            string connectionString =
                "Server=;Database=;User ID=;Password=;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString);

            return new(optionsBuilder.Options);
        }
    }
}
