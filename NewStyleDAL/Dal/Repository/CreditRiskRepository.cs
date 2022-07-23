using Dal.EfStructures;
using Dal.Repository.Base;
using Dal.Repository.Interfaces;
using Model.Entities;
using Microsoft.EntityFrameworkCore;


namespace Dal.Repository
{
    public class CreditRiskRepository : BaseRepository<CreditRisk>, ICreditRiskRepository
    {
        public CreditRiskRepository(ApplicationDbContext context) : base(context) { }

        internal CreditRiskRepository(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
