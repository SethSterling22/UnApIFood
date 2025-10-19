using Dapper;
using System.Data.SqlClient;
using UnApIFood.Models;
using UnApIFood.Utils;


namespace UnApIFood.Repositories
{
    public class UnivDAO
    {

        public async Task<University> GetUniversity(int id)
        {
            const string sqlQuery = "SELECT * FROM [University] WHERE Id = @Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            return await db.QuerySingleOrDefaultAsync<University>(sqlQuery, new { Id = id });
        }

        public async Task<List<University>> GetAll()
        {
            var sqlQuery = "SELECT * FROM [University]";
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var universities = await db.QueryAsync<University>(sqlQuery);
            return universities.ToList();
        }


        public async Task<University> Post(University university)
        {
        // Construir la consulta SQL
        const string sqlQuery = @"
            INSERT INTO [University] (Name, Address, Description, ImageUrl, CreatedBy, Created)
            VALUES (@Name, @Address, @Description, @ImageUrl, @CreatedBy, @Created);
            SELECT CAST(SCOPE_IDENTITY() AS int) AS Id";

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            var universityId = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, university);

            // Actualizar la universidad con el ID generado
            university.Id = universityId;

            // Devolver la universidad con el ID
            return university;
        }

        public async Task<University> Put(University university)
        {
            // Query de actualización
            const string sqlQuery = @"
                UPDATE [University]
                SET Name = @Name, Address = @Address, Description = @Description, ImageUrl = @ImageUrl, ModifiedBy = @ModifiedBy, Modified = @Modified
                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", university.Id);
            parameters.Add("@Name", university.Name);
            parameters.Add("@Address", university.Address);
            parameters.Add("@Description", university.Description);
            parameters.Add("@ImageUrl", university.ImageURL);
            parameters.Add("@ModifiedBy", university.ModifiedBy);
            parameters.Add("@Modified", DateTime.UtcNow);

            // Ejecutar la consulta en la base de datos
            using var db = new SqlConnection(ConfigUtil.ConnectionString);
            await db.ExecuteAsync(sqlQuery, parameters);
            return university;
        }


        public async Task DeleteUniversity(int id)
        {
            // const string sqlQuery = "DELETE FROM [University] WHERE Id = @Id";
            
            // // Ejecutar la consulta en la base de datos
            // using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            // {
            //     await db.ExecuteAsync(sqlQuery, new { Id = id });
            // }

            const string deleteUniversityQuery = "DELETE FROM [University] WHERE Id = @Id";
            const string deletePlacesQuery = "DELETE FROM [Place] WHERE UniversityId = @UniversityId";
            const string deleteMenusQuery = "DELETE FROM [Menu] WHERE PlaceId IN (SELECT Id FROM [Place] WHERE UniversityId = @UniversityId)";

            using (var db = new SqlConnection(ConfigUtil.ConnectionString))
            {
                await db.OpenAsync();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Eliminar los menús asociados a los lugares de la universidad
                        await db.ExecuteAsync(deleteMenusQuery, new { UniversityId = id }, transaction);

                        // Eliminar los lugares asociados a la universidad
                        await db.ExecuteAsync(deletePlacesQuery, new { UniversityId = id }, transaction);

                        // Eliminar la universidad
                        await db.ExecuteAsync(deleteUniversityQuery, new { Id = id }, transaction);

                        // Confirmar la transacción
                        transaction.Commit();
                    }
                    catch
                    {
                        // Si ocurrió un error, deshacer la transacción
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

