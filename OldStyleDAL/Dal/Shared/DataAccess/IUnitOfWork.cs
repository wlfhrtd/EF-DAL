using Dal.Entities;
using System;


namespace Dal.Shared.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository ArtistRepository { get; }

        IRepository<ArtistSkill> ArtistSkillRepository { get; }

        void Flush();
    }
}
