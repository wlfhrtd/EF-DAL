using System.Linq;
using Model.Entities;
using Dal.Repository.Base;
using Model.ViewModel;


namespace Dal.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<CustomerOrderViewModel> GetOrdersViewModel();
    }
}
