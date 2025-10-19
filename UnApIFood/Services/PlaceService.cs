using UnApIFood.Models;
using UnApIFood.Repositories;

namespace UnApIFood.Services
{
    public class PlacesService
    {
    private readonly PlaceDAO _placeDAO;

        public PlacesService(PlaceDAO placeDAO)
        {
            _placeDAO = placeDAO;
        }


        public async Task<Place> GetPlace(int id)
        {
            // Realizar validaciones adicionales si es necesario
            return await _placeDAO.GetPlace(id);
        }

        public async Task<List<Place>> GetAll()
        {
            List<Place> places = await _placeDAO.GetAll();
            return places;
        }

        // Traer todos los lugares de la Universidad especificada!!!! :)
        public async Task<List<Place>> GetAll(int UniversityId)
        {
            List<Place> places = await _placeDAO.GetAll(UniversityId);
            return places;
        }


        public async Task<Place> Post(Place place)
        {
            // Validar la entrada de la universidad
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            // Establecer fecha de creación
            place.Created = DateTime.UtcNow;

            // Llamar al DAO para guardar la universidad en la base de datos
            place = await _placeDAO.Post(place);

            // Devolver la universidad guardada
            return place;
        }


        public async Task<Place> Put(Place place)
        {
            place = await _placeDAO.Put(place);
            return place;
        }

        public async Task DeletePlace(int id)
        {
            // Realizar validaciones adicionales si es necesario
            await _placeDAO.DeletePlace(id);
        }

    }
}