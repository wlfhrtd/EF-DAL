using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dal.EfStructures;
using Model.Entities;
using Dal.Repository.Base;
using Dal.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Dal.Repository
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context) { }

        internal CarRepository(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public override IEnumerable<Car> FindAll()
            => Table
            .Include(c => c.MakeNavigation)
            .OrderBy(c => c.PetName);

        public IEnumerable<Car> FindAllBy(int makeId)
            => Table
            .Where(c => c.MakeId == makeId)
            .Include(c => c.MakeNavigation)
            .OrderBy(c => c.PetName);

        public override IEnumerable<Car> FindAllIgnoreQueryFilters()
            => Table
            .Include(c => c.MakeNavigation)
            .OrderBy(c => c.PetName)
            .IgnoreQueryFilters();

        public string GetPetNameById(int id)
        {
            SqlParameter parameterId = new()
            {
                ParameterName = "@carId",
                SqlDbType = SqlDbType.Int,
                Value = id,
            };
            SqlParameter parameterName = new()
            {
                ParameterName = "@petName",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output,
            };

            _ = Context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", parameterId, parameterName);

            return (string)parameterName.Value;
        }

        public override Car? FindOneById(int? id)
            => Table
            .IgnoreQueryFilters()
            .Where(c => c.Id == id)
            .Include(c => c.MakeNavigation)
            .FirstOrDefault();
    }
}
