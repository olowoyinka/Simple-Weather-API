using Weather.Core.Models;
using Weather.Data.GenericDB;

namespace Weather.Data.Repository
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
    }
}