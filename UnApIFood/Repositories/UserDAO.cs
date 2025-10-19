using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;


namespace UnApIFood.Repositories
{
    public class UserDAO
    {

        public async Task<User> GetUser(int id)
        {
            const string sqlQuery = "SELECT * FROM [User] WHERE Id = @Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            return await db.QuerySingleOrDefaultAsync<User>(sqlQuery, new { Id = id });
        }

        public async Task<List<User>> GetAll()
        {
            var sqlQuery = "SELECT * FROM [User]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var users = await db.QueryAsync<User>(sqlQuery);
            return users.ToList();
        }

        public async Task<User> Post(User user)
        {
            // Construir la consulta SQL
            const string sqlQuery = @"
                INSERT INTO [User] (Role, Email, Password, FirstName, LastName, LastLogin, RegisteredOn)
                VALUES (@Role, @Email, @Password, @FirstName, @LastName, @LastLogin, @RegisteredOn);
                SELECT CAST(SCOPE_IDENTITY() AS int) AS Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var userId = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, user);

            // Actualizar el usuario con el ID generado
            user.Id = userId;

            // Devolver el usuario con el ID
            return user;
        }

        public async Task<User> Put(User user)
        {
            // Realizar el query de actualización
            const string sqlQuery = @"
                UPDATE [User]
                SET Role = @Role, Email = @Email, Password = @Password, FirstName = @FirstName, LastName = @LastName, LastLogin = @LastLogin, RegisteredOn = @RegisteredOn
                WHERE Id = @Id";

            // Parámetros del query
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id);
            parameters.Add("@Role", user.Role);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@LastLogin", user.LastLogin);
            parameters.Add("@RegisteredOn", DateTime.UtcNow);

            // Ejecutar query para la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.ExecuteAsync(sqlQuery, parameters);
            return user;
        }

        public async Task DeleteUser(int id)
        {
            const string sqlQuery = "DELETE FROM [User] WHERE Id = @Id";
            
            // Ejecutar la consulta en la base de datos
            using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}

