using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;

namespace UnApIFood.Repositories
{
    public class UserFavDAO
    {
        public async Task<UserFavoritePlace> GetUserFav(int id)
        {
            const string sqlQuery = "SELECT * FROM [UserFavoritePlace] WHERE Id = @Id";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            return await db.QuerySingleOrDefaultAsync<UserFavoritePlace>(sqlQuery, new { Id = id });
        }

        public async Task<List<UserFavoritePlace>> GetAll()
        {
            var sqlQuery = "SELECT * FROM [UserFavoritePlace]";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var usersfav = await db.QueryAsync<UserFavoritePlace>(sqlQuery);
            return usersfav.ToList();
        }

        // BONUS: Traer lista de los lugares preferidos de las universidades por el usuario
        public async Task<List<UserFavoritePlace>> GetUserFavoritePlaces(int userId)
        {   
            var sqlQuery = @"SELECT uf.*
                FROM [UserFavoritePlace] uf
                INNER JOIN [Place] p ON uf.PlaceId = p.Id
                WHERE uf.UserId = @UserId";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var userFavoritePlaces = await db.QueryAsync<UserFavoritePlace>(sqlQuery, new { UserId = userId });
            return userFavoritePlaces.ToList();
        }

        public async Task<UserFavoritePlace> Post(UserFavoritePlace userfav)
        {
            const string sqlQuery = @"
                INSERT INTO [UserFavoritePlace] (UserId, PlaceId)
                VALUES (@UserId, @PlaceId);
                SELECT CAST(SCOPE_IDENTITY() AS int) AS Id";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var userfavId = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, userfav);

            userfav.Id = userfavId;
            return userfav;
        }

        public async Task<UserFavoritePlace> Put(UserFavoritePlace userfav)
        {
            const string sqlQuery = @"
                UPDATE [UserFavoritePlace]
                SET UserId = @UserId, PlaceId = @PlaceId
                WHERE Id = @Id";

            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.ExecuteAsync(sqlQuery, userfav);
            return userfav;
        }

        public async Task DeleteUserFav(int id)
        {
            const string sqlQuery = "DELETE FROM [UserFavoritePlace] WHERE Id = @Id";

            using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}