using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;

namespace UnApIFood.Controllers.V1
{
    public class UsersFavController : BaseController
    {

        private readonly UsersFavService _usersfavService;

        public UsersFavController(UsersFavService usersfavService)
        {
            _usersfavService = usersfavService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFav(int id)
        {
            try
            {
                var userfav = await _usersfavService.GetUserFav(id);

                if (userfav != null)
                {
                    return Ok(userfav);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserFavoritePlace>>> GetAll()
        {
            try
            {
                return Ok(await _usersfavService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // BONUS: Traer lista de los lugares de las universidades preferidas por el usuario e identificar el favorito de la listaª
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserFavoritePlaces(int userId)
        {
            var userFavoritePlaces = await _usersfavService.GetUserFavoritePlaces(userId);
            return Ok(userFavoritePlaces);
        }

        [HttpPost]
        public async Task<ActionResult<UserFavoritePlace>> CreateUser(UserFavoritePlace userfav)
        {
            try
            {
                if (userfav == null)
                {
                    return BadRequest("Se deben llenar todos los campos de la Universidad.");
                }

                var createdUserFav = await _usersfavService.Post(userfav);
                return createdUserFav;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserFavoritePlace>> UpdateUserFav(int id, UserFavoritePlace userfav)
        {
            try
            {
                if (id != userfav.Id)
                {
                    return BadRequest("El ID y el URL deben ser iguales");
                }

                var updateduserfav = await _usersfavService.Put(userfav);
                if (updateduserfav == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(updateduserfav);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserFav(int id)
        {
            try
            {
                await _usersfavService.DeleteUserFav(id);
                return Ok();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("No se encontró usuario con el Id dado"))
                {
                    // El usuario con el ID especificado no existe
                    return NotFound();
                }
                else
                {
                    // Otro tipo de error
                    return StatusCode(500, ex.Message);
                }
            }
        }
    }
}