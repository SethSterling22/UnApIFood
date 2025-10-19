using UnApIFood.Models;
using UnApIFood.Repositories;
using UnApIFood.Utils;

namespace UnApIFood.Services
{
    public class LoginService
    {
        private readonly LoginDAO _loginDAO;

        public LoginService(LoginDAO loginDAO)
        {
            _loginDAO = loginDAO;
        }

        public async Task<string> LoginAsync(Login login)
        {
            // Obtener los datos del usuario desde la base de datos
            Login storedLogin = await _loginDAO.GetLogin(login);

            // Verificar si los datos coinciden
            if (storedLogin != null && storedLogin.Password == login.Password)
            {
                return JWTUtil.GenerateJWT(login);
            }

            throw new Exception("invalid_credentials");
        }
    }
}
