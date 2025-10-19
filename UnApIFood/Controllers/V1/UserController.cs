using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;
using Newtonsoft.Json;

namespace UnApIFood.Controllers.V1
{
    public class UsersController : BaseController
    {

        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _usersService.GetUser(id);

                if (user != null)
                {
                    return Ok(user);
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

        // Traer una lista de Usuarios paginada
        [HttpGet("10")]
        public async Task<ActionResult<List<User>>> GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var users = await _usersService.GetAll();
                var totalCount = users.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var pagedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                var paginationInfo = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationInfo));

                return Ok(pagedUsers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Traer la lista de todos los usuarios
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            try
            {
                return Ok(await _usersService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Se deben llenar todos los campos de la Universidad.");
                }

                var createdUser = await _usersService.Post(user);
                return createdUser;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("El ID y el URL deben ser iguales");
                }

                var updateduser = await _usersService.Put(user);
                if (updateduser == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(updateduser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _usersService.DeleteUser(id);
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