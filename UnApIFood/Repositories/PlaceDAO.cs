using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;


namespace UnApIFood.Repositories
{
    public class PlaceDAO
    {

        public async Task<Place> GetPlace(int id)
        {
            const string sqlQuery = "SELECT * FROM [Place] WHERE Id = @Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            return await db.QuerySingleOrDefaultAsync<Place>(sqlQuery, new { Id = id });
        }
        
        // Traer todos los lugares en general (Para revisarlos)

        public async Task<List<Place>> GetAll()
        {
            var sqlQuery = "SELECT * FROM [Place]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var universities = await db.QueryAsync<Place>(sqlQuery);
            return universities.ToList();
        }

        public async Task<List<Place>> GetAll(int UniversityId)
        {   
            // Traer todos los lugares de la Universidad especificada!!!! :)
            var sqlQuery = "SELECT * FROM [Place] WHERE UniversityId = @UniversityId";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var places = await db.QueryAsync<Place>(sqlQuery, new { UniversityId });
            return places.ToList();
        }

        public async Task<Place> Post(Place place)
        {
        // Construir la consulta SQL
        const string sqlQuery = @"
            INSERT INTO [Place] (UniversityId, Name, Address, Schedule, PriceAverage, Description, ImageUrl, CreatedBy, Created)
            VALUES (@UniversityId, @Name, @Address, @Schedule, @PriceAverage, @Description, @ImageUrl, @CreatedBy, @Created);
            SELECT CAST(SCOPE_IDENTITY() AS int) AS Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var placeId = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, place);

            // Actualizar la universidad con el ID generado
            place.Id = placeId;

            // Devolver la universidad con el ID
            return place;
        }

        public async Task<Place> Put(Place place)
        {
            // Query de actualización
            const string sqlQuery = @"
                UPDATE [Place]
                SET Name = @Name, @UniversityId = UniversityId, Address = @Address, Schedule = @Schedule, PriceAverage = @PriceAverage, Description = @Description, ImageUrl = @ImageUrl, ModifiedBy = @ModifiedBy, Modified = @Modified
                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", place.Id);
            parameters.Add("@UniversityId", place.UniversityId);
            parameters.Add("@Name", place.Name);
            parameters.Add("@Description", place.Description);
            parameters.Add("@Schedule", place.Schedule);
            parameters.Add("@PriceAverage", place.PriceAverage);
            parameters.Add("@Address", place.Address);
            parameters.Add("@ImageUrl", place.ImageURL);
            parameters.Add("@ModifiedBy", place.ModifiedBy);
            parameters.Add("@Modified", DateTime.UtcNow);

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.ExecuteAsync(sqlQuery, parameters);
            return place;
        }

        public async Task DeletePlace(int id)
        {
            // const string sqlQuery = "DELETE FROM [Place] WHERE Id = @Id";
            
            // // Ejecutar la consulta en la base de datos
            // using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            // {
            //     await db.ExecuteAsync(sqlQuery, new { Id = id });
            // }

            const string deletePlaceQuery = "DELETE FROM [Place] WHERE Id = @Id";
            const string deleteMenusQuery = "DELETE FROM [Menu] WHERE PlaceId = @PlaceId";

            using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            {
                await db.OpenAsync();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Eliminar los menús asociados al lugar
                        await db.ExecuteAsync(deleteMenusQuery, new { PlaceId = id }, transaction);

                        // Eliminar el lugar
                        await db.ExecuteAsync(deletePlaceQuery, new { Id = id }, transaction);

                        // Confirmar la transacción
                        transaction.Commit();
                    }
                    catch
                    {
                        // Ocurrió un error, deshacer la transacción
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

