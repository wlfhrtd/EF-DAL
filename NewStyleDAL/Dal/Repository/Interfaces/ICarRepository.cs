using Dal.Repository.Base;
using Model.Entities;
using System.Collections.Generic;


namespace Dal.Repository.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        IEnumerable<Car> FindAllBy(int makeId);
        string GetPetNameById(int id);
    }
}
