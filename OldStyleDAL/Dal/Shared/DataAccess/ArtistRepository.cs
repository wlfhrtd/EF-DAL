using Dal.Context;
using Dal.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Dal.Shared.DataAccess
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext context) : base(context) { }


        /// <summary>
        /// Retrieves list of 20 new artists
        /// </summary>
        /// <param name="page">Page navigation</param>
        /// <returns>List of artists</returns>
        public IList<Artist> GetNewArtists(int page = 1)
        {
            var pageSize = 20;
            return Query(null, query => query.OrderByDescending(a => a.CreateDate), page, pageSize).ToList();
        }
    }
}
