using Model.Entities;
using Dal.Repository.Base;
using Dal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dal.EfStructures;
using System.Linq;
using Model.ViewModel;


namespace Dal.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        internal OrderRepository(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public IQueryable<CustomerOrderViewModel> GetOrdersViewModel()
            => Context.CustomerOrderViewModels!.AsQueryable();
    }
}
