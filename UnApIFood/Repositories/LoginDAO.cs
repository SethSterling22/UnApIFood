using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;

namespace UnApIFood.Repositories
{
    public class LoginDAO
    {
        public async Task<Login> GetLogin(Login login)
        {
        const string sqlQuery = "SELECT Email, Password, LastLogin FROM [User] WHERE Email = @Email";

        using var db = new SqlConnection(ConfigUtil.ConnectionString);
        var result = await db.QuerySingleOrDefaultAsync<Login>(sqlQuery, new { Email = login.Email });

        if (result != null)
        {
            // Actualizar el valor de LastLogin con la fecha actual
            var LastLogin = DateTime.UtcNow;

            // Actualizar el registro en la base de datos con el nuevo valor de LastLogin
            const string updateQuery = "UPDATE [User] SET LastLogin = @LastLogin WHERE Email = @Email";
            await db.ExecuteAsync(updateQuery, new { LastLogin, Email = result.Email });
        }

        return result;
        }
    }
}