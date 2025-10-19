using Microsoft.AspNetCore.Mvc;
using UnApIFood.Models;
using UnApIFood.Services;

namespace UnApIFood.Controllers.V1
{
    public class MenusController : BaseController
    {

        private readonly MenusService _menusService;

        public MenusController(MenusService menusService)
        {
            _menusService = menusService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            try
            {
                var user = await _menusService.GetMenu(id);

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

        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetAll()
        {
            try
            {
                return Ok(await _menusService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> CreateMenu(Menu menu)
        {
            try
            {
                if (menu == null)
                {
                    return BadRequest("Se deben llenar todos los campos del Menú.");
                }

                var createdMenu = await _menusService.Post(menu);
                return createdMenu;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Menu>> UpdateMenu(int id, Menu menu)
        {
            try
            {
                if (id != menu.Id)
                {
                    return BadRequest("El ID y el URL deben ser iguales");
                }

                var updatedMenu = await _menusService.Put(menu);
                if (updatedMenu == null)
                {
                    return NotFound("No se encontró el Menú con ese Id.");
                }

                return Ok(updatedMenu);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                await _menusService.DeleteMenu(id);
                return Ok();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("No se encontró Menu con el Id dado"))
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