using UnApIFood.Models;
using UnApIFood.Repositories;

namespace UnApIFood.Services
{
    public class UniversitiesService
    {
    private readonly UnivDAO _universityDAO;

        public UniversitiesService(UnivDAO universityDAO)
        {
            _universityDAO = universityDAO;
        }

        public async Task<University> GetUniversity(int id)
        {
            // Realizar validaciones adicionales si es necesario
            return await _universityDAO.GetUniversity(id);
        }

        public async Task<List<University>> GetAll()
        {
            List<University> universities = await _universityDAO.GetAll();
            return universities;
        }

        public async Task<University> Post(University university)
        {
            // Validar la entrada de la universidad
            if (university == null)
            {
                throw new ArgumentNullException(nameof(university));
            }

            // Establecer fecha de creación
            university.Created = DateTime.UtcNow;

            // Llamar al DAO para guardar la universidad en la base de datos
            university = await _universityDAO.Post(university);

            // Devolver la universidad guardada
            return university;
        }

        public async Task<University> Put(University university)
        {
            university = await _universityDAO.Put(university);
            return university;
        }

        public async Task DeleteUniversity(int id)
        {
            // Realizar validaciones adicionales si es necesario
            await _universityDAO.DeleteUniversity(id);
        }
    }
}