using UnApIFood.Models;
using UnApIFood.Repositories;

namespace UnApIFood.Services
{
    public class UsersService
    {
    private readonly UserDAO _userDAO;

        public UsersService(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<User> GetUser(int id)
        {
            // Realizar validaciones adicionales si es necesario
            return await _userDAO.GetUser(id);
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await _userDAO.GetAll();
            return users;
        }


        public async Task<User> Post(User user)
        {
            // Validar la entrada de la universidad
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Llamar al DAO para guardar la universidad en la base de datos y reenviar los datos
            user = await _userDAO.Post(user);
            return user;
        }

        public async Task<User> Put(User user)
        {

            user = await _userDAO.Put(user);
            return user;
        }


        public async Task DeleteUser(int id)
        {
            // Realizar validaciones adicionales si es necesario
            await _userDAO.DeleteUser(id);
        }
    }
}