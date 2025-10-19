using UnApIFood.Models;
using UnApIFood.Repositories;

namespace UnApIFood.Services
{
    public class MenusService
    {
    private readonly MenuDAO _menuDAO;

        public MenusService(MenuDAO menuDAO)
        {
            _menuDAO = menuDAO;
        }

        public async Task<Menu> GetMenu(int id)
        {
            // Realizar validaciones adicionales si es necesario
            return await _menuDAO.GetMenu(id);
        }

        public async Task<List<Menu>> GetAll()
        {
            List<Menu> universities = await _menuDAO.GetAll();
            return universities;
        }

        public async Task<Menu> Post(Menu menu)
        {
            // Validar la entrada de la universidad
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            // Establecer fecha de creación
            menu.Created = DateTime.UtcNow;

            // Llamar al DAO para guardar la universidad en la base de datos
            menu = await _menuDAO.Post(menu);

            // Devolver la universidad guardada
            return menu;
        }

        public async Task<Menu> Put(Menu menu)
        {
            menu = await _menuDAO.Put(menu);
            return menu;
        }

        public async Task DeleteMenu(int id)
        {
            // Realizar validaciones adicionales si es necesario
            await _menuDAO.DeleteMenu(id);
        }

    }
}