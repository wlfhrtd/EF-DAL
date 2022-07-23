using Dal.Entities;
using System.Collections.Generic;


namespace Dal.Shared.DataAccess
{
    public interface IArtistRepository
    {
        IList<Artist> GetNewArtists(int page = 1);
    }
}
