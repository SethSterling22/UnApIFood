using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;


namespace UnApIFood.Repositories
{
    public class MenuDAO
    {

        public async Task<Menu> GetMenu(int id)
        {
            const string sqlQuery = "SELECT * FROM [Menu] WHERE Id = @Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            return await db.QuerySingleOrDefaultAsync<Menu>(sqlQuery, new { Id = id });
        }

        public async Task<List<Menu>> GetAll()
        {
            var sqlQuery = "SELECT * FROM [Menu]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var universities = await db.QueryAsync<Menu>(sqlQuery);
            return universities.ToList();
        }

        public async Task<Menu> Post(Menu menu)
        {
        // Construir la consulta SQL
        const string sqlQuery = @"
            INSERT INTO [Menu] (PlaceId, Category, Name, Description, Price, ImageURL, CreatedBy, Created, ModifiedBy, Modified)
            VALUES (@PlaceId, @Category, @Name, @Description, @Price, @ImageURL, @CreatedBy, @Created, @ModifiedBy, @Modified);
            SELECT CAST(SCOPE_IDENTITY() AS int) AS Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var menuId = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, menu);

            // Actualizar la universidad con el ID generado
            menu.Id = menuId;

            // Devolver la universidad con el ID
            return menu;
        }

        public async Task<Menu> Put(Menu menu)
        {
            // Realizar el query de actualización
            const string sqlQuery = @"
                UPDATE [Menu]
                SET PlaceId = @PlaceId, Category = @Category, Name = @Name,  Description = @Description,  Price = @Price, ImageUrl = @ImageUrl, ModifiedBy = @ModifiedBy, Modified = @Modified
                WHERE Id = @Id";

            // Parámetros del query
            var parameters = new DynamicParameters();
            parameters.Add("@Id", menu.Id);
            parameters.Add("@PlaceId", menu.PlaceId);
            parameters.Add("@Category", menu.Category);
            parameters.Add("@Name", menu.Name);
            parameters.Add("@Description", menu.Description);
            parameters.Add("@Price", menu.Price);
            parameters.Add("@ImageUrl", menu.ImageURL);
            parameters.Add("@ModifiedBy", menu.ModifiedBy);
            parameters.Add("@Modified", DateTime.UtcNow);

            // Ejecturar consulta a la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.ExecuteAsync(sqlQuery, parameters);
            return menu;
        }

        public async Task DeleteMenu(int id)
        {
            const string sqlQuery = "DELETE FROM [Menu] WHERE Id = @Id";
            
            // Ejecutar la consulta en la base de datos
            using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}

