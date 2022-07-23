using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;


namespace Dal.EfStructures
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
            string connectionString = @"server=;Database=;User Id=;Password=";
            optionsBuilder.UseSqlServer(connectionString);
            Console.WriteLine($"Connecting to: {connectionString}");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
