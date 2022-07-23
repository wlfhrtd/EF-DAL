using System.Collections.Generic;
using System.Linq;
using Dal.EfStructures;
using Model.Entities;
using Dal.Repository.Base;
using Dal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dal.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        internal CustomerRepository(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public override IEnumerable<Customer> FindAll()
            => Table
            .Include(c => c.Orders)
            .OrderBy(c => c.PersonalInformation.LastName);
    }
}
