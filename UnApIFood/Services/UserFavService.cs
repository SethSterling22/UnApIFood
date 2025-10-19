using UnApIFood.Models;
using UnApIFood.Repositories;

namespace UnApIFood.Services
{
    public class UsersFavService
    {
    private readonly UserFavDAO _userfavDAO;

        public UsersFavService(UserFavDAO userfavDAO)
        {
            _userfavDAO = userfavDAO;
        }

        public async Task<UserFavoritePlace> GetUserFav(int id)
        {
            // Realizar validaciones adicionales si es necesario
            return await _userfavDAO.GetUserFav(id);
        }

        public async Task<List<UserFavoritePlace>> GetAll()
        {
            List<UserFavoritePlace> usersfav = await _userfavDAO.GetAll();
            return usersfav;
        }
        
        // BONUS: Traer lista de los lugares de las universidades preferidas por el usuario e identificar el favorito de la lista
        public async Task<List<UserFavoritePlace>> GetUserFavoritePlaces(int userId)
        {
            return await _userfavDAO.GetUserFavoritePlaces(userId);
        }


        public async Task<UserFavoritePlace> Post(UserFavoritePlace userfav)
        {
            // Validar la entrada de la universidad
            if (userfav == null)
            {
                throw new ArgumentNullException(nameof(userfav));
            }

            // Llamar al DAO para guardar la universidad en la base de datos y reenviar los datos
            userfav = await _userfavDAO.Post(userfav);
            return userfav;
        }

        public async Task<UserFavoritePlace> Put(UserFavoritePlace userfav)
        {

            userfav = await _userfavDAO.Put(userfav);
            return userfav;
        }


        public async Task DeleteUserFav(int id)
        {
            // Realizar validaciones adicionales si es necesario
            await _userfavDAO.DeleteUserFav(id);
        }
    }
}