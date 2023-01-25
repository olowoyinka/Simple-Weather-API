using Weather.Core.Models;
using Weather.Data.Data;
using Weather.Data.GenericDB;

namespace Weather.Data.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context, context.RefreshTokens) { }
    }
}