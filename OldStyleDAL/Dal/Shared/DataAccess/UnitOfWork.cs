using Dal.Context;
using Dal.Entities;
using System;


namespace Dal.Shared.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;

        private IArtistRepository artistRepository;

        private IRepository<ArtistSkill> artistSkillRepository;


        public UnitOfWork(string connectionString)
        {
            context = new ApplicationDbContext(connectionString);
        }

        /// <summary>
        /// Allows class to be instantiated with injected DbContext
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IArtistRepository ArtistRepository
        {
            get
            {
                if (artistRepository == null)
                {
                    artistRepository = new ArtistRepository(context);
                }
                return artistRepository;
            }
        }

        public IRepository<ArtistSkill> ArtistSkillRepository
        {
            get
            {
                if (artistSkillRepository == null)
                {
                    artistSkillRepository = new Repository<ArtistSkill>(context);
                }
                return artistSkillRepository;
            }
        }

        public void Flush()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
